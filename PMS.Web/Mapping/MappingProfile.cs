using AutoMapper;
using PMS.Web.Entities;
using PMS.Web.Models;
using PMS.Web.Models.Requests;

namespace PMS.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<HospitalUser, HospitalUserModel>();
            CreateMap<HospitalUserModel, HospitalUser>();
            CreateMap<Medication, MedicationModel>();
            CreateMap<MedicationModel, Medication>();
            CreateMap<Procedure, ProcedureModel>();
            CreateMap<ProcedureModel, Procedure>();
            CreateMap<Diagnosis, DiagnosisModel>();
            CreateMap<DiagnosisModel, Diagnosis>();
            CreateMap<Allergy, AllergyModel>();
            CreateMap<AllergyModel, Allergy>();
            CreateMap<AppUser, UserModel>();
            CreateMap<UserModel, AppUser>();
            CreateMap<Patient, PatientModel>();
            CreateMap<PatientModel, Patient>();
            CreateMap<Scheduling, SchedulingModel>();
            CreateMap<SchedulingModel, Scheduling>();
            CreateMap<SchedulingHistory, SchedulingHistoryModel>();
            CreateMap<SchedulingHistoryModel, SchedulingHistory>();
            CreateMap<TimeSlot, TimeSlotModel>();
            CreateMap<TimeSlotModel, TimeSlot>();
            CreateMap<PatientAllergy, PatientAllergyModel>();
            CreateMap<PatientAllergyModel, PatientAllergy>();
            CreateMap<PatientVisit, PatientVisitModel>();
            CreateMap<PatientVisitModel, PatientVisit>();
            CreateMap<PatientMedication, PatientMedicationModel>();
            CreateMap<PatientMedicationModel, PatientMedication>();
            CreateMap<PatientProcedureModel, PatientProcedure>();
            CreateMap<PatientProcedure, PatientProcedureModel>();
            CreateMap<MessageModel, Message>();
            CreateMap<Message, MessageModel>();
            CreateMap<ReplyMessageModel, ReplyMessage>();
            CreateMap<ReplyMessage, ReplyMessageModel>();
            CreateMap<PatientDiagnosis, PatientDiagnosisModel>();
            CreateMap<PatientDiagnosisModel, PatientDiagnosis>();
            CreateMap<RefreshToken, RefreshTokenModel>();
            CreateMap<RefreshTokenModel, RefreshToken>();
            CreateMap<AppUser, UserLoginModel>();
            CreateMap<UserLoginModel, AppUser>();
        }
    }
}
