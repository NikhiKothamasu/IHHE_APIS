using System.ComponentModel.DataAnnotations;

namespace Project_Apis.Models
{
    public class AppointmentData
    {
        [Key]
        public Guid AppointmentId { get; set; }
        public string labtest {  get; set; }
        public string diagonsis { get; set; }
        public string medication {  get; set; }
        public int weight {  get; set; }
        public string prescritionnote { get; set; }

        public string blood_pressure { get; set; }
        public int heart_rate { get; set; }
    }
}
