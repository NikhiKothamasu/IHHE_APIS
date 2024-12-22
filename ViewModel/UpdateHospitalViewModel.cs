namespace Project_Apis.ViewModel
{
    public class UpdateHospitalViewModel
    {
        
        public Guid HospitalId { get; set; }
        public string? HospitalName { get; set; }

        
        public string? HospitalEmail { get; set; }

        public string? HospitalPhoneNumber { get; set; }

        public string? AvailableFacilities { get; set; }

        public string? HospitalType { get; set; }

        public string? HospitalAddress { get; set; }

       
        public string? HospitalOwnershipType { get; set; }
    }
}
