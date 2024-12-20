using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_Apis.Models
{
    public class Patient
    {
       
        [Key]
        public int PatientId { get; set; }

        public string? PatientName { get; set; }

        public int Age { get; set; }
        public string? FathersName { get; set; }
        public string ?Physician { get; set; }
        public string ?Gender { get; set; }

        public int ?Height {  get; set; }

        public string ?BloodType { get; set; }

        public string ?Email { get; set; }
        public string ?MobileNumber { get; set; }

        public string ?EmergencyContact { get; set; }

        public string ?Account_Status { get; set; }

        public string Password {  get; set; }

        public ICollection<Appointments> Appointments { get; set; }
    }
}
