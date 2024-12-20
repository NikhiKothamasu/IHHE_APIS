using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Apis.Models
{
    public class Doctor
    {
        [Key]
        public Guid Doctor_Id { get; set; } 
        public string? Name { get; set; } 
        public string? FieldOfStudy { get; set; }

        [ForeignKey("Hospital")]
        public Guid HospitalId { get; set; } 
        public string? AssociatedHospital { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Account_Status {  get; set; }
    }
}
