using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.Web.Entities;
using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class PatientSchedulingService : IPatientSchedulingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PatientSchedulingService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<IEnumerable<SchedulingModel>> GetSchedulededAppointments(Guid patientId)
        {
            var schedulingList = await _context.Schedulings.Include(h => h.SchedulingsHistory)
                .Include(s => s.TimeSlot).Include(o => o.HospitalUser).ThenInclude(p => p.AppUser).Include(v => v.PatientVisit).Include(p => p.Patient)
                .ThenInclude(p => p.AppUser).Where(s => s.PId == patientId).ToListAsync();
            List<SchedulingModel> schedulingListModel = new();
            foreach (var scheduling in schedulingList)
            {
                schedulingListModel.Add(_mapper.Map<SchedulingModel>(scheduling));
            }
            return schedulingListModel;
        }

        public async Task<IEnumerable<SchedulingModel>> GetPhysicianSchedulededAppointments(Guid EmployeeId)
        {
            var schedulingList = await _context.Schedulings.Include(h=>h.SchedulingsHistory).Include(s => s.TimeSlot).Include(v=>v.PatientVisit)
                .Include(h => h.HospitalUser).ThenInclude(h => h.AppUser).Include(p => p.Patient).ThenInclude(s => s.AppUser).Where(s => s.EmployeeId == EmployeeId).ToListAsync();
            List<SchedulingModel> schedulingListModel = new();
            foreach (var scheduling in schedulingList)
            {
                schedulingListModel.Add(_mapper.Map<SchedulingModel>(scheduling));
            }
            return schedulingListModel;
        }

        public async Task<IEnumerable<SchedulingModel>> GetAppointmentDetails(Guid PatientId)
        {
            List<SchedulingModel> schedulingModel = new();
            List<Scheduling> scheduling = await _context.Schedulings.Include(s => s.SchedulingsHistory).Include(s=>s.PatientVisit).Include(t => t.TimeSlot).
                Include(h => h.HospitalUser).ThenInclude(h => h.AppUser).Include(p => p.Patient).ThenInclude(s => s.AppUser).Where(s => s.PId == PatientId).ToListAsync();
            foreach (var appointment in scheduling)
            {
                schedulingModel.Add(_mapper.Map<SchedulingModel>(appointment));
            }

            return schedulingModel;
        }

        public async Task<IEnumerable<SchedulingHistoryModel>> GetAppointmentEditHistory(Guid scheduleId)
        {
            List<SchedulingHistoryModel> schedulingHistoryModel = new();
            List<SchedulingHistory> schedulingHistory = await _context.SchedulingHistory.Include(s => s.AppUser).Where(s => s.ScheduleId == scheduleId).OrderByDescending(t=>t.EditedTime).ToListAsync();
            foreach (var history in schedulingHistory)
            {
                schedulingHistoryModel.Add(_mapper.Map<SchedulingHistoryModel>(history));
            }

            return schedulingHistoryModel;

        }

        public async Task<IEnumerable<TimeSlotModel>> AvailableTimeSlot(Guid employeeId, DateTime selecetedDate)
        {
            List<TimeSlotModel> timeSlotModel = new();
            List<TimeSlot> timeSlots;
            var res = await (from o in _context.TimeSlot
                             join j in _context.Schedulings
                             on o.TimeSlotId equals j.TimeSlotId
                             where j.EmployeeId == employeeId && j.AppointmentDate.Date == selecetedDate.Date && (j.Status == "1" || j.Status == "2")
                             select new TimeSlot
                             {
                                 TimeSlotId = o.TimeSlotId,
                                 Timing = o.Timing,
                             }).ToListAsync();
            timeSlots = await _context.TimeSlot.Where(s => !res.Contains(s)).ToListAsync();
            List<TimeSlot> timeSlotsEdited = new();
            if (selecetedDate.Date == DateTime.Today.Date)
            {
                int a = DateTime.Now.Hour;
                foreach (var timeSlot in timeSlots)
                {
                    string[] time = timeSlot.Timing.Split('-');
                    string[] temp = time[0].Split(':');
                    int timeSlotHour = int.Parse(temp[0]);
                    if (Int32.Parse(temp[0]) < 12 && temp[1].Contains("PM"))
                    {
                        a = Int32.Parse(temp[0]) - 12;
                        if (timeSlotHour <= a)
                            timeSlotsEdited.Add(timeSlot);
                    }
                    if (timeSlotHour <= a)
                        timeSlotsEdited.Add(timeSlot);

                }
            }

            timeSlots = timeSlots.Where(s => !timeSlotsEdited.Contains(s)).ToList();

            foreach (var timeSlot in timeSlots)
            {
                timeSlotModel.Add(_mapper.Map<TimeSlotModel>(timeSlot));
            }
            return timeSlotModel;

        }

        public async Task<int> AddSchedule(SchedulingModel schedulingModel)
        {
            int i = 0;
            if (schedulingModel != null)
            {
                schedulingModel.AppointmentDate = GetAppointmentDateTime(schedulingModel.AppointmentDate, schedulingModel.TimeSlotId);
                schedulingModel.ScheduleId = Guid.NewGuid();
                if (schedulingModel.Status == "")
                {
                    schedulingModel.Status = "1";
                }
                Scheduling scheduling = _mapper.Map<Scheduling>(schedulingModel);
                var res = await _context.Schedulings.Where(s => s.AppointmentDate.Date == scheduling.AppointmentDate.Date && s.PId == scheduling.PId && s.TimeSlotId == scheduling.TimeSlotId).FirstOrDefaultAsync();
                if (res == null)
                {
                    await _context.Schedulings.AddAsync(scheduling);
                    i = _context.SaveChanges();
                }
                else 
                {
                    i = -1;
                    return i;
                }
            }
            return i;

        }
        public async Task<int> EditSchedule(SchedulingModel schedulingModel, string userId, string role)
        {
            int i = 0;
            if (schedulingModel != null)
            {
                Scheduling scheduling;
                if (schedulingModel != null)
                {

                    List<SchedulingHistory> schedulingHistoryList = await _context.SchedulingHistory.Where(s => s.ScheduleId == schedulingModel.ScheduleId).ToListAsync();

                    if (schedulingHistoryList.Count > 0)
                    {
                        int s = schedulingHistoryList.Count - 1;
                        foreach (var scheduleHistory in schedulingModel.SchedulingsHistory)
                        {

                            scheduleHistory.AppUserId = userId;
                            scheduleHistory.PreviousDetails = schedulingHistoryList[s].ReasonForEdit;
                            scheduleHistory.EditedTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                        }
                    }
                    else
                    {
                        foreach (var scheduleHistory in schedulingModel.SchedulingsHistory)
                        {
                            scheduleHistory.AppUserId = userId;
                            scheduleHistory.PreviousDetails = schedulingModel.MeetingTitle;
                            scheduleHistory.EditedTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                        }
                    }


                    scheduling = _mapper.Map<Scheduling>(schedulingModel);
                    scheduling.AppointmentDate = GetAppointmentDateTime(scheduling.AppointmentDate, scheduling.TimeSlotId);
                    _context.Schedulings.Update(scheduling);
                    i = await _context.SaveChangesAsync();
                }
            }
            return i;

        }

        public async Task<int> UpdateScheduleStatus(Guid scheduleId, string scheduleStatus)
        {
            int i = 0;
            var Schedule = await _context.Schedulings.Include(o => o.TimeSlot).Where(s => s.ScheduleId == scheduleId).FirstOrDefaultAsync();
            if (Schedule != null)
            {
                Schedule.Status = scheduleStatus;
                var scheduling = _mapper.Map<Scheduling>(Schedule);
                _context.Schedulings.Update(scheduling);
                i = await _context.SaveChangesAsync();
            }
            return i;
        }

        public async Task<int> DeleteSchedule(Guid scheduleId)
        {
            int i = 0;
            Scheduling scheduling = await _context.Schedulings.Where(s => s.ScheduleId == scheduleId).FirstOrDefaultAsync();
            if (scheduling != null)
            {
                _context.Schedulings.Remove(scheduling);
                i = await _context.SaveChangesAsync();
            }
            return i;
        }

        public int UpdateSchedulingHistory(SchedulingHistoryModel schedulingHistoryModel)
        {
            int i = 0;
            if (schedulingHistoryModel != null)
            {
                schedulingHistoryModel.EditedTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                SchedulingHistory schedulingHistory = _mapper.Map<SchedulingHistory>(schedulingHistoryModel);
                _context.SchedulingHistory.Add(schedulingHistory);
                i = _context.SaveChanges();
            }
            return i;
        }

        public async Task<IEnumerable<SchedulingModel>> GetAllSchedules(DateTime appointmentDate)
        {
            List<Scheduling> schedules = await _context.Schedulings.Where(s => s.AppointmentDate.Date == appointmentDate.Date && s.Status != "3").OrderBy(t => t.TimeSlotId)
                .Include(t => t.TimeSlot)
                .Include(v => v.PatientVisit)
                .Include(h => h.HospitalUser).ThenInclude(a => a.AppUser)
                .Include(p => p.Patient).ThenInclude(a => a.AppUser).ToListAsync();
            return _mapper.Map<IEnumerable<SchedulingModel>>(schedules);

        }
        public async Task<SchedulingModel> GetScheduleById(Guid ScheduleId)
        {
            Scheduling schedule = await _context.Schedulings.Where(s => s.ScheduleId==ScheduleId)
                .Include(v => v.PatientVisit).ThenInclude(d=>d.PatientDiagnoses)
                .Include(m=>m.PatientVisit).ThenInclude(m=>m.PatientMedications)
                .Include(m=>m.PatientVisit).ThenInclude(m=>m.PatientProcedures)
                .Include(h => h.HospitalUser).ThenInclude(a => a.AppUser)
                .Include(p => p.Patient).ThenInclude(a => a.AppUser).FirstOrDefaultAsync();
            return _mapper.Map<SchedulingModel>(schedule);
        }

        public DateTime GetAppointmentDateTime(DateTime appointmentDate,int timeSlotId)
        {
            var timing = _context.TimeSlot.Find(timeSlotId);
            var timeSlot = timing.Timing;
            var hours = int.Parse(timeSlot.Split(':')[0]);
            if (timeSlot.Contains("PM") && hours < 12)
            {
                hours += 12;
            }
            var exactDateTime = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day, hours, 0, 0);
            return exactDateTime;
        }
        private void SendStatusInfoToPatient(Scheduling Schedule)
        {
            var patient = _context.Patients.Include(p => p.AppUser).Where(o => o.PId == Schedule.PId).FirstOrDefault();
            var physician = _context.HospitalUser.Include(p => p.AppUser).Where(o => o.EmployeeId == Schedule.EmployeeId).FirstOrDefault();

            MailMessage msg = new()
            {
                From = new MailAddress("citiusprojectteam3@gmail.com")
            };
            msg.To.Add(patient.AppUser.Email);
            msg.Subject = "Appointment Status";
            if (Schedule.Status == "2")
            {
                msg.Body = "Dear " + patient.AppUser.FirstName + "," + "<br />" + "   Your request for appointment with <strong>Dr." + physician.AppUser.FirstName + "</strong> on <strong>"
                            + Schedule.AppointmentDate.ToLongDateString() + "</strong> at <strong>" + Schedule.TimeSlot.Timing + "</strong> is approved." + "<br /><br />"
                            + "Regards," + "<br />" + "<strong>CT Hospital</strong>";
            }
            else
            {
                msg.Body = "Dear " + patient.AppUser.FirstName + "," + "<br />" + "   Your request for appointment with <strong>Dr." + physician.AppUser.FirstName + "</strong> on <strong>"
                            + Schedule.AppointmentDate.ToLongDateString() + "</strong> at <strong>" + Schedule.TimeSlot.Timing + "</strong> is rejected." + "<br /><br />"
                            + "Regards," + "<br />" + "<strong>CT Hospital</strong>";
            }

            msg.IsBodyHtml = true;
            SmtpClient smtp = new();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential network = new()
            {
                UserName = "citiusprojectteam3@gmail.com",
                Password = "CT#12345"
            };
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = network;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        
    }
}
