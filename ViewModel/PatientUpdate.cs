namespace Project_Apis.ViewModel
{
    public class PatientUpdate
    {
       public int PatientId {  get; set; }
        public string PatientName { get; set; }

        public int Age { get; set; }
        public string FathersName { get; set; }
        public string Physician { get; set; }
        public string Gender { get; set; }

        public string BloodType { get; set; }

        public string Email { get; set; }
        public string MobileNumber { get; set; }

        public string EmergencyContact { get; set; }
    }
}
