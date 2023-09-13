using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class PatientModel
    {
        public Guid PId { get; set; }
        public string PatientId { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Ethinicity { get; set; }
        public string LanguagesKnown { get; set; }
        public string HomeAddress { get; set; }
        public string EmergencyInfoFirstName { get; set; }
        public string EmergencyInfoLastName { get; set; }
        public string EmergencyInfoRelationship { get; set; }
        public string EmergencyInfoEmailAddress { get; set; }
        public string EmergencyCountrycode { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public string EmergencyHomeAddress { get; set; }
        public bool EmergencyPatientPortal { get; set; }
        public Guid AppUserId { get; set; }
     
        public UserModel AppUser { get; set; }
        public ICollection<PatientAllergyModel> Allergies{get; set;}
        public virtual ICollection<SchedulingModel> Schedulings { get; set; }

    }
}
