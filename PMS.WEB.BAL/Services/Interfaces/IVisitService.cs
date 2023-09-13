using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IVisitService
    {
        Task<IEnumerable<PatientVisitModel>> GetAllVisits(string role,Guid employeeId);
        Task<IEnumerable<PatientVisitModel>> GetAllVisits(Guid PId);
        Task<int> AddPatientVisit(PatientVisitModel patientVisitModel);
        Task<int> UpdatePatientVisit(PatientVisitModel patientVisitModel);
    }
}
