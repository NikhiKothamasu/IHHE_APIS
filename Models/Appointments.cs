using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_Apis.Models;

namespace Project_Apis.Models
{
    public class Appointments
    {
        [Key]
        public Guid AppointmentId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        
        public string HospitalName { get; set; }

        public string Doctor { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public TimeOnly AppointmentTime { get; set; }

        public string AppointmentNote { get; set; }

        public string Status { get; set; }

        public DateTime RequestDateTime { get; set; }

        public Patient Patient { get; set; }




    }
}
