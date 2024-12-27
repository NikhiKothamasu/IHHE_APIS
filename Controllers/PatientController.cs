using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Apis.Data;
using Project_Apis.Models;
using Project_Apis.ViewModel;
using System.Net.Mail;
using System.Net;

namespace Project_Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly ApiDbContext apiDbContext;
       
        public PatientController(ApiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
            
        }

        //Intial Hospitals Search 
        [HttpGet("HospitalsList")]
        public IActionResult GetHosiptalList()
        {
            try
            {
                var results = apiDbContext.Hospitals.Where(h=>h.AccountStatus=="Active").Select(h => h.HospitalName).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        //Doctor List Based on Selection
        [HttpGet("DoctorsList")]
        public IActionResult GetDoctorsList([FromQuery] string HospitalName)
        {
            try
            {
                var results = apiDbContext.Doctors.Where(d => d.AssociatedHospital == HospitalName && d.Account_Status=="Active").Select(d => new { d.Name, d.FieldOfStudy }).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

       

        // Appoinmet Insertion To Database
        [HttpPost("BookAppoinment")]
        public IActionResult InsertAppoitnments(ViewAppointments obj)
        {
            try
            {
                var existingAppointment = apiDbContext.Appointments.FirstOrDefault(a => a.PatientId == obj.PatientId &&
                                 a.AppointmentDate == obj.AppointmentDate &&
                                 a.AppointmentTime == obj.AppointmentTime);
                if (existingAppointment != null)
                {
                    return Conflict(new { message = "You already have an appointment at this date and time." });
                }
                var insert_data = new Appointments()
                {
                    AppointmentId = Guid.NewGuid(),
                    PatientId = obj.PatientId,
                    HospitalName = obj.HospitalName,
                    Doctor = obj.Doctor,
                    AppointmentDate = obj.AppointmentDate,
                    AppointmentTime = obj.AppointmentTime,
                    AppointmentNote = obj.AppointmentNote,
                    Status = "Pending",
                    RequestDateTime = DateTime.Now

                };
                apiDbContext.Appointments.Add(insert_data);
                apiDbContext.SaveChanges();
                return Ok(insert_data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }

        }

        //Appointments Fetch
        [HttpGet("AppointmentsFetch")]
        public IActionResult GetAppointments([FromQuery] int PatientId)
        {
            try
            {
                var results = apiDbContext.Appointments.Where(p => p.PatientId == PatientId).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        //MedicalData
        [HttpGet("AppointmentsData")]
        public async Task<IActionResult> GetAppointmentDetails([FromQuery] int patientId)
        {
            try
            {
                var results = from a in apiDbContext.Appointments
                              join ad in apiDbContext.AppointmentDatas on
                              a.AppointmentId equals ad.AppointmentId
                              where a.PatientId == patientId
                              select new
                              {
                                  a.HospitalName, a.AppointmentDate, a.Doctor,
                                  ad.labtest, ad.diagonsis, ad.medication,
                                  ad.weight, ad.blood_pressure, ad.heart_rate
                              };
                return Ok(results.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        //Profile_Personal_Information
        [HttpGet("PersonalInformation")]
        public IActionResult GetPersonalInformation([FromQuery] int PatientId)
        {
            try
            {
                var results = apiDbContext.Patients.Where(p => p.PatientId==PatientId &&p.Account_Status.Equals("Active")).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        //Patient Account Updation
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateAccount(Patient obj)
        {
            try
            {
                var patient = apiDbContext.Patients.FirstOrDefault(p => p.PatientId == obj.PatientId);
                if (patient == null)
                {
                    return NotFound(new { message = "Patient not found" });
                }
                patient.PatientName = obj.PatientName;
                patient.Gender = obj.Gender;
                patient.Age = obj.Age;
                patient.FathersName = obj.FathersName;
                patient.MobileNumber  = obj.MobileNumber;
                patient.EmergencyContact = obj.EmergencyContact;
                patient.Physician = obj.Physician;
                patient.BloodType = obj.BloodType;
                patient.Email = obj.Email;
                apiDbContext.SaveChanges();
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        //Patient  Update Password
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword([FromQuery] int PatientId, [FromQuery] string currentPassword, [FromQuery] string newPassword)
        {
            try
            {
                var patient = apiDbContext.Patients.FirstOrDefault(p => p.PatientId==PatientId);

                if (patient == null)
                {
                    return NotFound("Patient ID not found");
                }
                if (patient.Password != currentPassword)
                {
                    return NotFound("Incorrect Password Entered");
                }
                patient.Password = newPassword;
                apiDbContext.SaveChanges();

                return Ok(new { status = "success", message = "Password Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        // Patient Account Deletion
        [HttpDelete("DeletePatient")]
        public IActionResult DeleteAccount([FromQuery] int PatientId)
        {
            try
            {

                var patient = apiDbContext.Patients.Find(PatientId);
                patient.Account_Status = "Inactive";
                apiDbContext.SaveChanges();
                return Ok(new { message = "Patient Details deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        } 
    }
}


