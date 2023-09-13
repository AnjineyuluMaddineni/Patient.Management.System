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
    public class VisitService : IVisitService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUserRoles> _roleManager;
        public VisitService(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppUserRoles> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // public async Task<IEnumerable<PatientVisitModel>> GetAllVisits()
        // {
        //     var visits = await _context.PatientVisits.Include(p => p.PatientDiagnoses).ThenInclude(master=>master.Diagnosis)
        //                                                 .Include(p => p.PatientProcedures).ThenInclude(master=>master.Procedure)
        //                                                 .Include(m => m.PatientMedications).ThenInclude(master => master.Medication)
        //                                                 .Include(s=>s.Scheduling).ThenInclude(p=>p.Patient).ThenInclude(u=>u.AppUser).OrderByDescending(s=>s.VisitDate).
        //                                                 ToListAsync();
        //     return _mapper.Map<IEnumerable<PatientVisitModel>>(visits);
        // }

        public async Task<IEnumerable<PatientVisitModel>> GetAllVisits(string role, Guid employeeId)
        {
            if (role == UserRolesModels.Physician)
            {
                var visits = await _context.PatientVisits.Include(p => p.PatientDiagnoses).ThenInclude(master => master.Diagnosis)
                                                            .Include(p => p.PatientProcedures).ThenInclude(master => master.Procedure)
                                                            .Include(m => m.PatientMedications).ThenInclude(master => master.Medication)
                                                            .Include(s => s.Scheduling).ThenInclude(p => p.Patient).ThenInclude(u => u.AppUser).Where(s=>s.Scheduling.EmployeeId == employeeId).OrderByDescending(s => s.VisitDate).
                                                            ToListAsync();

                return _mapper.Map<IEnumerable<PatientVisitModel>>(visits);
            }
            var visit = await _context.PatientVisits.Include(p => p.PatientDiagnoses).ThenInclude(master => master.Diagnosis)
                                                            .Include(p => p.PatientProcedures).ThenInclude(master => master.Procedure)
                                                            .Include(m => m.PatientMedications).ThenInclude(master => master.Medication)
                                                            .Include(s => s.Scheduling).ThenInclude(p => p.Patient).ThenInclude(u => u.AppUser).OrderByDescending(s => s.VisitDate).
                                                            ToListAsync();

            return _mapper.Map<IEnumerable<PatientVisitModel>>(visit);
        }

        public async Task<IEnumerable<PatientVisitModel>> GetAllVisits(Guid PId)
        {
            var visits = await _context.PatientVisits.Include(p => p.PatientDiagnoses).ThenInclude(master => master.Diagnosis)
                                                        .Include(p => p.PatientProcedures).ThenInclude(master => master.Procedure)
                                                        .Include(m => m.PatientMedications).ThenInclude(master => master.Medication)
                                                        .Include(s => s.Scheduling).ThenInclude(p => p.Patient).ThenInclude(u => u.AppUser)
                                                        .Where(v => v.Scheduling.Patient.PId == PId).OrderByDescending(s => s.VisitDate)
                                                        .ToListAsync();
            return _mapper.Map<IEnumerable<PatientVisitModel>>(visits);
        }

        public async Task<int> AddPatientVisit(PatientVisitModel patientVisitModel)
        {
            int i = 0;  
            if (patientVisitModel != null)
            {
                PatientVisit patientVisit = _mapper.Map<PatientVisit>(patientVisitModel);
                await _context.PatientVisits.AddAsync(patientVisit);
                i = await _context.SaveChangesAsync();
                return i;
            }
            return i;
        }
        public async Task<int> UpdatePatientVisit(PatientVisitModel patientVisitModel)
        {
            int i = 0;
            if (patientVisitModel != null)
            {
                PatientVisit patientVisit = _mapper.Map<PatientVisit>(patientVisitModel);
                List<Guid> deleteData = new();
                foreach(var diagnosis in patientVisitModel.PatientDiagnoses)
                {
                    deleteData.Add(diagnosis.DiagnosisId);
                }
                if (deleteData.Count > 0 || (patientVisit.PatientDiagnoses.Count == 0))
                {
                    var data = await _context.PatientDiagnoses.Where(x =>x.VisitId.Equals(patientVisit.VisitId) && ((deleteData.Count <= 0) || !deleteData.Contains(x.DiagnosisId))).ToListAsync();
                    _context.RemoveRange(data);
                }
                deleteData.Clear();
                foreach (var procedure in patientVisitModel.PatientProcedures)
                {
                    deleteData.Add(procedure.ProcedureId);
                }
                if (deleteData.Count > 0 || (patientVisit.PatientProcedures.Count == 0))
                {
                    var procData = await _context.PatientProcedures.Where(x => x.VisitId.Equals(patientVisit.VisitId) && ((deleteData.Count <= 0) || !deleteData.Contains(x.ProcedureId))).ToListAsync();
                    _context.RemoveRange(procData);
                }
                deleteData.Clear();
                foreach (var medication in patientVisitModel.PatientMedications)
                {
                    deleteData.Add(medication.MId);
                }
                if (deleteData.Count > 0||(patientVisit.PatientMedications.Count==0))
                {
                    var data = await _context.PatientMedications.Where(x => x.VisitId.Equals(patientVisit.VisitId) && ((deleteData.Count <= 0) || !deleteData.Contains(x.MId))).ToListAsync();
                    _context.RemoveRange(data);
                }
               
                deleteData.Clear();

                _context.PatientVisits.Update(patientVisit);
                i = await _context.SaveChangesAsync();
                return i;
            }
            return i;
        }
    }
}
