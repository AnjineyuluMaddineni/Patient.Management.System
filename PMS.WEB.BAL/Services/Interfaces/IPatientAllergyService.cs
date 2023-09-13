using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IPatientAllergyService
    {
        Task<IEnumerable<PatientAllergyModel>> GetPatientAllergies(Guid PId);
        Task<int> AddPatientAllergy(PatientAllergyModel patientAllergyModel);
        Task<int> UpdatePatientAllergy(PatientAllergyModel patientAllergyModel);
        Task<int> DeletePatientAllergy(Guid allergyInfoId);
    }
}
