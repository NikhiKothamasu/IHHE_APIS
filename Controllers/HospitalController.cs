using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Apis.Data;
using Project_Apis.Models;
using Project_Apis.ViewModel;

namespace Project_Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private ApiDbContext apiDbContext;
        public HospitalController(ApiDbContext apiDbContext) {
            this.apiDbContext = apiDbContext;
        }

        [HttpGet("Appointments")]
        public IActionResult AppointmentsList([FromQuery] Guid HospitalId)
        {
            try
            {
                var hospitalname = apiDbContext.Hospitals.Where(h => h.HospitalId.Equals(HospitalId)).Select(h => h.HospitalName).FirstOrDefault();
                var result = apiDbContext.Appointments.Where(h => h.HospitalName.Equals(hospitalname));
                return Ok(result);
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPut("AppointmentsApproval")]
        public IActionResult AppointmentsApproval([FromQuery] Guid AppointmentId, [FromQuery] string status)
        {
            try
            {
                var appointment = apiDbContext.Appointments.FirstOrDefault(a => a.AppointmentId == AppointmentId);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {AppointmentId} not found.");
                }
                appointment.Status = status;
                apiDbContext.SaveChanges();
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while updating the appointment status. " + ex.Message);
            }
        }


        [HttpPost("AppointmentData")]
        public IActionResult updateAppointmentData(AppointmentData obj)
        {
            try
            {
                var Appointment=apiDbContext.Appointments.FirstOrDefault(a =>a.AppointmentId.Equals(obj.AppointmentId));

                Appointment.Status = "Visited";
                
                AppointmentData ad = new AppointmentData()
                {
                    AppointmentId = obj.AppointmentId,
                    labtest = obj.labtest,
                    diagonsis = obj.diagonsis,
                    medication = obj.medication,
                    weight = obj.weight,
                    prescritionnote = obj.prescritionnote,
                    blood_pressure = obj.blood_pressure,
                    heart_rate = obj.heart_rate,
                };
                apiDbContext.Add(ad);
                apiDbContext.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured", details = ex.Message });
            }


        }

        [HttpGet("DoctorsList")]
        public IActionResult GetDoctors([FromQuery] Guid HospitalId)
        {
            try
            {
                var hospitalName = apiDbContext.Hospitals.Where(h => h.HospitalId.Equals(HospitalId)).Select(h => h.HospitalName).FirstOrDefault();
                var results = apiDbContext.Doctors.Where(d => d.AssociatedHospital.Equals(hospitalName)).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }


        [HttpGet("HospitalProfile")]
        public IActionResult GetProfile([FromQuery] Guid HospitalId)
        {
            try
            {
                var profile = apiDbContext.Hospitals.Where(h => h.HospitalId.Equals(HospitalId));
                return Ok(profile);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPut("UpdateProfile")]
        public IActionResult updateProfile(UpdateHospitalViewModel obj)
        {
            try
            {
                var hospital = apiDbContext.Hospitals.FirstOrDefault(p=>p.HospitalId.Equals(obj.HospitalId));
                if (hospital == null)
                {
                    return NotFound(new { message = "Hospital not found" });
                }
                hospital.HospitalName = obj.HospitalName;
                hospital.HospitalEmail = obj.HospitalEmail;
                hospital.HospitalPhoneNumber = obj.HospitalPhoneNumber;
                hospital.AvailableFacilities = obj.AvailableFacilities;
                hospital.HospitalType = obj.HospitalType;
                hospital.HospitalAddress = obj.HospitalAddress;
                hospital.HospitalOwnershipType = obj.HospitalOwnershipType;
                
                apiDbContext.SaveChanges();
                return Ok(hospital);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }



        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword([FromQuery] Guid HospitalId, [FromQuery] string currentPassword, [FromQuery] string newPassword)
        {
            try
            {
                var hospital = apiDbContext.Hospitals.FirstOrDefault(h => h.HospitalId.Equals(HospitalId));

                if (hospital == null)
                {
                    return NotFound("Patient ID not found");
                }
                if (hospital.HospitalAccountPassword != currentPassword)
                {
                    return NotFound("Incorrect Password Entered");
                }
                hospital.HospitalAccountPassword = newPassword;
                apiDbContext.SaveChanges();
                return Ok(new { status = "success", message = "Password Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }
             
        [HttpDelete("DeleteHospital")]
        public IActionResult deleteAccount([FromQuery] Guid hospitalId)
        {
            try
            { 
                var hospital = apiDbContext.Hospitals.Find(hospitalId);
                hospital.AccountStatus = "Inactive";
                apiDbContext.SaveChanges();
                return Ok(new { message = "Hospital Details deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }
    }
}
