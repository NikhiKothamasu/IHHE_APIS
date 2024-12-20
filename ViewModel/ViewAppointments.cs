using System.ComponentModel.DataAnnotations;

namespace Project_Apis.ViewModel
{
    public class ViewAppointments
    {
        
        public int PatientId { get; set; }

        public string HospitalName { get; set; }

        public string Doctor { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public TimeOnly AppointmentTime { get; set; }

        public string AppointmentNote { get; set; }


      


    }
}
