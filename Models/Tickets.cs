using System.ComponentModel.DataAnnotations;

namespace Project_Apis.Models
{
    public class Tickets
    {
        [Key]
        public Guid TicketId { get; set; }
        public string? UserType { get; set; }
        public string? UserEmail { get; set; }
        public string? UserId { get; set; }
        public string? Issue { get; set; }

        public string? Status { get; set; }

    }
}
