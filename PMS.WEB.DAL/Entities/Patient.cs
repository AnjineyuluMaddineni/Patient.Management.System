using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class Patient
    {
        [Key]
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
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<PatientAllergy> Allergies { get; set; }
        public ICollection<Scheduling> Schedulings { get; set; }
    }
}
