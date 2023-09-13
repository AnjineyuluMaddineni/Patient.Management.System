using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IPatientService
    {
        Task<int> Register(UserModel userModel);
        Task<int> AddDemographic(PatientModel patientModel);
        Task<int> AddPatientAllergy(PatientAllergyModel patientAllergyModel);
        Task<int> UpdatePatientAllergy(PatientAllergyModel patientAllergyModel);
        Task<IEnumerable<UserModel>> GetAllPatients();
        Task<UserModel> GetUserById(UserModel user);
        Task<PatientModel> GetPatientDetails(string appUserId);
        Task<PatientModel> GetPatientDetailsById (Guid pId);
        Task<int> DeletePatientAllergy(Guid allergyInfoId);
        Task<IEnumerable<PatientAllergyModel>> GetPatientAllergies(Guid PId);

    }
}
