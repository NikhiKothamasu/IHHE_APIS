using Project_Apis.Models;

namespace ProjectApis.Models
{
    public class Hospital
    {
        public Guid HospitalId { get; set; }

        public string? HospitalName { get; set; }

        public string? FounderName { get; set; }

        public string? HospitalEmail { get; set; }

        public string? HospitalPhoneNumber { get; set; }

        public string? AvailableFacilities { get; set; }

        public string? HospitalType { get; set; }

        public string? HospitalAddress { get; set; }

        public string? HospitalRegion { get; set; }

        public string? HospitalAccountPassword { get; set; }

        public DateTime? HospitalEstablishedDate { get; set; }

        public string? HospitalOwnershipType { get; set; }

        public string AccountStatus { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
