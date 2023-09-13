using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IAdminService
    {
        Task<int> AddAllergy(AllergyModel newAllergy);
        Task<int> EditAllergy(AllergyModel newAllergy);
        Task<int> DeleteAllergy(Guid aId);
        Task<int> DeleteMedication(Guid mId);
        Task<int> AddMedication(MedicationModel newMedications);
        Task<int> EditMedication(MedicationModel newMedications);
        Task<IEnumerable<AllergyModel>> AllAllergies();
        Task<IEnumerable<MedicationModel>> AllMedication();
        Task<IEnumerable<ProcedureModel>> AllProcedure();
        Task<IEnumerable<DiagnosisModel>> AllDiagnosis();
        Task<int> AddProcedure(ProcedureModel newProcedures);
        Task<int> EditProcedure(ProcedureModel newProcedure);
        Task<int> DeleteProcedure(Guid procedureId);
        Task<int> AddDiagnosis(DiagnosisModel newDiagnosis);
        Task<int> EditDiagnosis(DiagnosisModel newDiagnosis);
        Task<int> DeleteDiagnosis(Guid diagnosisId);
    }
}
