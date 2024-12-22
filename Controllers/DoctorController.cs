using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Apis.Data;
using Project_Apis.ViewModel;

namespace Project_Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApiDbContext apiDbContext;
        
        public DoctorController(ApiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }

        //View Profile
        [HttpGet("DoctorProfile")]
        public IActionResult Get_Doctor_Registration_Data(Guid Doctor_Id)
        {
            var data = apiDbContext.Doctors.FirstOrDefault(p => p.Doctor_Id.Equals(Doctor_Id) && p.Account_Status.Equals("Active"));
            if (data == null)
            {
                return NotFound("Id not found");
            }
            var doctor_data = apiDbContext.Doctors.ToList();
            return Ok(data);
        }

        //Update Profile
        [HttpPut("UpdationDoctorData")]
        public IActionResult UpdateAccount([FromQuery] Guid Doctor_Id, [FromBody] DoctorViewModel model)
        {
            try
            {
                var data = apiDbContext.Doctors.FirstOrDefault(id => id.Doctor_Id == Doctor_Id && id.Account_Status == "Active");
                if (data == null)
                {
                    return NotFound($"Doctor with ID {Doctor_Id} not found.");
                }
                data.AssociatedHospital = model.AssociatedHospital;
                data.Email = model.Email;
                data.FieldOfStudy = model.FieldOfStudy;
                data.Mobile = model.Mobile;
                data.Name = model.Name;
                apiDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }


        //Change Password
        [HttpPut("ChangePassword")]
        public IActionResult Update_Doctor_Password([FromQuery] Guid Doctor_Id, [FromQuery] string Doctor_Old_Password, [FromQuery] string Doctor_Updated_Password)
        {
            try
            {

                var data = apiDbContext.Doctors.FirstOrDefault(p => p.Doctor_Id == Doctor_Id);
                if (data == null)
                {
                    return NotFound(new { message = "Doctor ID not found" });

                }
                if (data.Password != Doctor_Old_Password)
                {
                    return BadRequest(new { message = "Incorrect Password Entered" });

                }
                data.Password = Doctor_Updated_Password;
                apiDbContext.SaveChanges();

                return Ok(new { message = "Password updated successfully." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //Account Deletion
        [HttpDelete("DeleteAccount")]
        public IActionResult Delete_Account([FromQuery] Guid Id)
        {
            try
            {
                if (apiDbContext == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not initialized.");
                }
                var doctor = apiDbContext.Doctors.FirstOrDefault(h => h.Doctor_Id == Id);

                if (doctor == null)
                {
                    return NotFound($"No active doctor account found with ID {Id}.");
                }
                doctor.Account_Status = "Inactive";
                apiDbContext.SaveChanges();
                return Ok(new
                {
                    Message = $"Doctor account with ID {Id} has been successfully deactivated.",
                    Status = "Deactivated"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                });
            }
        }

        //Get Scheduled Appointments
        [HttpGet("doctorappointments")]
        public IActionResult DoctorAppointments([FromQuery] Guid doctorId)
        {
            try
            {
                
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
                var doctorName = apiDbContext.Doctors
                    .Where(d => d.Doctor_Id == doctorId )
                    .Select(d => d.Name)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(doctorName))
                {
                    return NotFound(new { message = "Doctor not found" });
                }
                var results = from a in apiDbContext.Appointments
                              join p in apiDbContext.Patients on a.PatientId equals p.PatientId
                              where a.Doctor == doctorName && a.AppointmentDate >= currentDate && a.Status.Equals("Approved")
                              select new
                              {
                                 
                                  a.AppointmentDate,
                                  a.AppointmentTime,
                                  a.AppointmentNote,
                                  a.Doctor,
                                  PatientName = p.PatientName,
                                  patientId=p.PatientId
                              };

                var appointmentDetails = results.ToList();
                return Ok(appointmentDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }



    }
}
