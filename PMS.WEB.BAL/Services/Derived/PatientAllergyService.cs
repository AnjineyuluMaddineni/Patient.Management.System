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
    public class PatientAllergyService : IPatientAllergyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUserRoles> _roleManager;
        public PatientAllergyService(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppUserRoles> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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

    }
}
