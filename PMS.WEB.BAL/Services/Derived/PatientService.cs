using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.Web.Entities;
using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUserRoles> _roleManager;
        public PatientService(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppUserRoles> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserModel>> GetAllPatients()
        {
            var patients = await _context.AppUsers.ToListAsync();
            return _mapper.Map<IEnumerable<UserModel>>(patients);
        }

        public async Task<int> Register(UserModel userModel)
        {
            AppUser user = _mapper.Map<AppUser>(userModel);
            var userExists = await _userManager.FindByEmailAsync(userModel.Email);
            if (userExists != null)
                return 0;
            IdentityResult result = await _userManager.CreateAsync(user, userModel.PasswordHash);

            if (result.Succeeded)
            {
                Patient patientUser = new();
                patientUser.AppUserId = user.Id;
                patientUser.PId = Guid.NewGuid();
                patientUser.PatientId = "PT201";
                await _context.Patients.AddAsync(patientUser);
                await _context.SaveChangesAsync();
                if (!await _roleManager.RoleExistsAsync(UserRolesModels.Patient))
                {
                    AppUserRoles roles = new();
                    roles.Name = UserRolesModels.Patient;
                    roles.Description = "Perform Patient Operations";
                    IdentityResult roleResult = await _roleManager.CreateAsync(roles);
                    if (!roleResult.Succeeded)
                    {
                        return 0;
                    }

                }
                await _userManager.AddToRoleAsync(user, UserRolesModels.Patient);
                return 1;
            }
            return 0;
        }
        public async Task<UserModel> GetUserById(UserModel user)
        {
            var resUser = await this._context.AppUsers.FirstOrDefaultAsync(d => d.PhoneNumber == user.PhoneNumber || d.Email == user.Email);
            return _mapper.Map<UserModel>(resUser);
        }
        public async Task<PatientModel> GetPatientDetails(string appUserId)
        {

            PatientModel patientModel = new();
            Patient patient = await _context.Patients.Include(patient => patient.AppUser).Where(s => s.AppUserId == appUserId).FirstOrDefaultAsync();
            if (patient != null)
            {

                patientModel = _mapper.Map<PatientModel>(patient);
                return patientModel;
            }
            else
                return patientModel;
        }

        public async Task<int> AddDemographic(PatientModel patientModel)
        {
            Patient patient = await _context.Patients.Include(p => p.AppUser).Where(S => S.AppUserId == patientModel.AppUserId.ToString()).FirstOrDefaultAsync();
            if (patientModel != null && patient != null)
            {
                patient.AppUser.Title = patientModel.AppUser.Title;
                patient.AppUser.FirstName = patientModel.AppUser.FirstName;
                patient.AppUser.LastName = patientModel.AppUser.LastName;
                patient.AppUser.PhoneNumber = patientModel.AppUser.PhoneNumber;
                patient.AppUser.BirthDate = patientModel.AppUser.BirthDate;
                patient.AppUser.CountryCode = patientModel.AppUser.CountryCode;
                patient.AppUser.Email = patientModel.AppUser.Email;
                patient.AppUser.NormalizedEmail = patientModel.AppUser.Email.ToUpper();

                patient.Gender = patientModel.Gender;
                patient.Race = patientModel.Race;
                patient.Ethinicity = patientModel.Ethinicity;
                patient.LanguagesKnown = patientModel.LanguagesKnown;
                patient.HomeAddress = patientModel.HomeAddress;
                patient.EmergencyHomeAddress = patientModel.EmergencyHomeAddress;
                patient.EmergencyInfoEmailAddress = patientModel.EmergencyInfoEmailAddress;
                patient.EmergencyInfoFirstName = patientModel.EmergencyInfoFirstName;
                patient.EmergencyInfoLastName = patientModel.EmergencyInfoLastName;
                patient.EmergencyInfoRelationship = patientModel.EmergencyInfoRelationship;
                patient.EmergencyPatientPortal = patientModel.EmergencyPatientPortal;
                patient.EmergencyCountrycode = patientModel.EmergencyCountrycode;
                patient.EmergencyPhoneNumber = patientModel.EmergencyPhoneNumber;

                _context.Patients.Update(patient);
                int i = await _context.SaveChangesAsync();
                return i;
            }
            return 0;
        }

        public async Task<int> AddPatientAllergy(PatientAllergyModel patientAllergyModel)
        {
            int i = 0;
            if (patientAllergyModel != null)
            {

                PatientAllergy newPatientAllergy = await _context.PatientAllergies.Where(s => s.AId == patientAllergyModel.AId && s.PId == patientAllergyModel.PId).FirstOrDefaultAsync();
                if (newPatientAllergy == null)
                {
                    newPatientAllergy = _mapper.Map<PatientAllergy>(patientAllergyModel);
                    await _context.PatientAllergies.AddAsync(newPatientAllergy);
                    i = await _context.SaveChangesAsync();
                    return i;
                }
                return -1;
            }
            return i;
        }
        public async Task<int> UpdatePatientAllergy(PatientAllergyModel patientAllergyModel)
        {
            int i = 0;
            if (patientAllergyModel != null)
            {
                PatientAllergy newPatientAllergy = _mapper.Map<PatientAllergy>(patientAllergyModel);
                _context.PatientAllergies.Update(newPatientAllergy);
                i = await _context.SaveChangesAsync();
                return i;
            }
            return i;
        }
        public async Task<int> DeletePatientAllergy(Guid allergyInfoId)
        {
            int i = 0;
            PatientAllergy newPatientAllergy = await _context.PatientAllergies.FindAsync(allergyInfoId);
            if (newPatientAllergy != null)
            {
                _context.PatientAllergies.Remove(newPatientAllergy);
                i = await _context.SaveChangesAsync();
            }
            return i;
        }

        public async Task<IEnumerable<PatientAllergyModel>> GetPatientAllergies(Guid PId)
        {

            IEnumerable<PatientAllergyModel> patientAllergyModel;
            IEnumerable<PatientAllergy> patient;
            patient = await _context.PatientAllergies.Include(patient => patient.Allergy).Where(s => s.PId == PId).ToListAsync();
            if (patient != null)
            {

                patientAllergyModel = _mapper.Map<IEnumerable<PatientAllergyModel>>(patient);
                return patientAllergyModel;
            }
            else
                return null;
        }

        public async Task<PatientModel> GetPatientDetailsById(Guid pId)
        {
            PatientModel patientModel = new();
            Patient patient = await _context.Patients.Include(o => o.AppUser).Where(p => p.PId == pId).FirstOrDefaultAsync();
            if (patient != null)
            {

                patientModel = _mapper.Map<PatientModel>(patient);
                return patientModel;
            }
            else
                return patientModel;
        }
    }
}
