using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.Web.Entities;
using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AllergyModel>> AllAllergies()
        {
            List<Allergy> allergies = await _context.Allergies.ToListAsync();
            return _mapper.Map<IEnumerable<AllergyModel>>(allergies);
        }
        public async Task<int> AddAllergy(AllergyModel newAllergy)
        {
            Allergy allergy = _mapper.Map<Allergy>(newAllergy);
            await _context.Allergies.AddAsync(allergy);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> EditAllergy(AllergyModel newAllergy)
        {
            Allergy allergy = _mapper.Map<Allergy>(newAllergy);
            _context.Allergies.Update(allergy);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> DeleteAllergy(Guid aId)
        {
            Allergy allergy = _context.Allergies.Find(aId);
            if (allergy != null)
            {
                _context.Allergies.Remove(allergy);
                var res = await _context.SaveChangesAsync();
                return res;
            }
            return 0;
        }
        public async Task<IEnumerable<MedicationModel>> AllMedication()
        {
            List<Medication> medications = await _context.Medications.ToListAsync();
            var medicationsList = _mapper.Map<IEnumerable<MedicationModel>>(medications);
            return medicationsList;
        }
        public async Task<int> AddMedication(MedicationModel newMedications)
        {
            Medication medications = _mapper.Map<Medication>(newMedications);
            await _context.Medications.AddAsync(medications);
            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<int> EditMedication(MedicationModel newMedications)
        {
            Medication medications = _mapper.Map<Medication>(newMedications);
            _context.Medications.Update(medications);
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteMedication(Guid mId)
        {
            Medication medication = _context.Medications.Find(mId);
            if (medication != null)
            {
                _context.Medications.Remove(medication);
                var res = await _context.SaveChangesAsync();
                return res;
            }
            return 0;
        }
        public async Task<IEnumerable<ProcedureModel>> AllProcedure()
        {
            List<Procedure> procedures = await _context.Procedures.ToListAsync();
            var proceduresList = _mapper.Map<IEnumerable<ProcedureModel>>(procedures);
            return proceduresList;
        }
        public async Task<int> AddProcedure(ProcedureModel newProcedures)
        {
            Procedure procedures = _mapper.Map<Procedure>(newProcedures);
            await _context.Procedures.AddAsync(procedures);
            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<int> EditProcedure(ProcedureModel newProcedure)
        {
            Procedure procedures = _mapper.Map<Procedure>(newProcedure);
            _context.Procedures.Update(procedures);
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteProcedure(Guid procedureId)
        {
            Procedure procedure = _context.Procedures.Find(procedureId);
            if (procedure != null)
            {
                _context.Procedures.Remove(procedure);
                var res = await _context.SaveChangesAsync();
                return res;
            }
            return 0;
        }

        public async Task<IEnumerable<DiagnosisModel>> AllDiagnosis()
        {
            List<Diagnosis> diagnosis = await _context.Diagnoses.ToListAsync();
            var diagnosisList = _mapper.Map<IEnumerable<DiagnosisModel>>(diagnosis);
            return diagnosisList;
        }
        public async Task<int> AddDiagnosis(DiagnosisModel newDiagnosis)
        {
            Diagnosis diagnosis = _mapper.Map<Diagnosis>(newDiagnosis);
            await _context.Diagnoses.AddAsync(diagnosis);
            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<int> EditDiagnosis(DiagnosisModel newDiagnosis)
        {

            Diagnosis diagnosis = _mapper.Map<Diagnosis>(newDiagnosis);
            _context.Diagnoses.Update(diagnosis);
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteDiagnosis(Guid diagnosisId)
        {
            Diagnosis diagnosis = _context.Diagnoses.Find(diagnosisId);
            if (diagnosis != null)
            {
                _context.Diagnoses.Remove(diagnosis);
                var res = await _context.SaveChangesAsync();
                return res;
            }
            return 0;
        }
    }
}
