using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Apis.ViewModel
{
    public class DoctorViewModel
    {

        public string? Name { get; set; }
        public string? FieldOfStudy { get; set; }

       
        public string? AssociatedHospital { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
    }
}
