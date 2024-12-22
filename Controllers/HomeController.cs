using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Apis.Data;
using Project_Apis.Models;
using Project_Apis.ViewModel;
using System.Net.Mail;
using System.Net;
using System.Numerics;

namespace Project_Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApiDbContext apiDbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApiDbContext apiDbContext, ILogger<HomeController> logger)
        {
            this.apiDbContext = apiDbContext;
            _logger = logger;
        }

        [HttpPost("PatientRegistration")]
        public IActionResult UserRegistartion(PatientViewModel patient)
        {
            try
            {
                Patient obj = new Patient()
                {
                    PatientName = patient.PatientName,
                    Age = patient.Age,
                    FathersName = patient.FathersName,
                    Physician = patient.Physician,
                    Gender = patient.Gender,
                    Height = patient.Height,
                    BloodType = patient.BloodType,
                    Email = patient.Email,
                    MobileNumber = patient.MobileNumber,
                    EmergencyContact = patient.EmergencyContact,
                    Account_Status = "Active",
                    Password = patient.PatientName + "@" + patient.Age
                };
                var result=apiDbContext.Patients.Where(p => p.Email.Equals(patient.Email)).FirstOrDefault();
                if (result != null)
                {
                    return Conflict(new { message = "You already registered with this Email" });
                }
                apiDbContext.Add(obj);
                apiDbContext.SaveChanges();
                SendEmail(obj.Email, obj.Password);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("DoctorRegistration")]
        public IActionResult DoctorRegistration(DoctorViewModel doctor)
        {
            try
            {
                var hospitalId = apiDbContext.Hospitals
                    .Where(h => h.HospitalName.Equals(doctor.AssociatedHospital))
                    .Select(h => h.HospitalId)
                    .FirstOrDefault();

                if (hospitalId == Guid.Empty)
                {
                    return BadRequest(new { message = "Associated Hospital not found." });
                }
                Doctor obj = new Doctor()
                {
                    Doctor_Id = Guid.NewGuid(), 
                    Name = doctor.Name,
                    FieldOfStudy = doctor.FieldOfStudy,
                    HospitalId = hospitalId,
                    AssociatedHospital = doctor.AssociatedHospital,
                    Mobile = doctor.Mobile,
                    Email = doctor.Email,
                    Password = doctor.Name + "@" + doctor.Mobile.Substring(0, 5),
                    Account_Status = "Active"
                };
                var result = apiDbContext.Doctors
                    .FirstOrDefault(p => p.Email.Equals(doctor.Email));

                if (result != null)
                {
                    return Conflict(new { message = "You are already registered with this Email" });
                }
                apiDbContext.Add(obj);
                apiDbContext.SaveChanges();
                SendEmail(obj.Email, obj.Password);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }


        [HttpPost("HospitalRegistration")]
        public IActionResult CreateHospital([FromBody] HospitalViewModel hospitalViewModel)
        {
            if (hospitalViewModel == null)
            {
                return BadRequest("Hospital data is required.");
            }


            var hospital = new Hospital
            {
                HospitalId = Guid.NewGuid(),
                HospitalName = hospitalViewModel.HospitalName,
                FounderName = hospitalViewModel.FounderName,
                HospitalEmail = hospitalViewModel.HospitalEmail,
                HospitalPhoneNumber = hospitalViewModel.HospitalPhoneNumber,
                AvailableFacilities = hospitalViewModel.AvailableFacilities,
                HospitalType = hospitalViewModel.HospitalType,
                HospitalAddress = hospitalViewModel.HospitalAddress,
                HospitalRegion = hospitalViewModel.HospitalRegion,
                HospitalAccountPassword = hospitalViewModel.HospitalName + "@" + 123,
                HospitalEstablishedDate = hospitalViewModel.HospitalEstablishedDate,
                HospitalOwnershipType = hospitalViewModel.HospitalOwnershipType,
                AccountStatus = "active",
            };
            var result = apiDbContext.Hospitals
                   .FirstOrDefault(p => p.HospitalEmail.Equals(hospital.HospitalEmail));

            if (result != null)
            {
                return Conflict(new { message = "You are already registered with this Email" });
            }
            apiDbContext.Hospitals.Add(hospital);
            apiDbContext.SaveChanges();
            return Ok(hospital);
        }


        //E-mail Generator
        private void SendEmail(string recipientEmail, string password)
        {
            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, 
                    Credentials = new NetworkCredential("revanthuppula0503@gmail.com", "lbgm bxiy tkuo gxtn"), 
                    EnableSsl = true, 
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@gmail.com"),
                    Subject = "Login Credentials from IHHE",
                    Body = $"Thanks for registering with IHHE. Your login credentials are as follows:\n\n" +
                           $"Username: {recipientEmail}\n" +
                           $"Password: {password}\n\n" +
                           "Please change your password once you log in.",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(recipientEmail);
                smtpClient.Send(mailMessage);
                _logger.LogInformation("Email Sent");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error sending email: {ex.Message}");
            }
        }

        //Login Authentication
        [HttpGet("UserAuthentication")]
        public IActionResult UserAuthentication([FromQuery] string usertype, [FromQuery] string username, [FromQuery] string password)
        {
            if (usertype.Equals("Patient", StringComparison.OrdinalIgnoreCase))
            {
                var result = apiDbContext.Patients.Where(p => p.Email.Equals(username)).FirstOrDefault();

                if (result != null)
                {
                    if (result.Account_Status.Equals("active", StringComparison.OrdinalIgnoreCase))
                    {
                        if (result.Password.Equals(password))
                        {
                            return Ok(new { message = "Authentication successful", patientId = result.PatientId });
                        }
                        else
                        {
                            return Unauthorized(new { message = "Invalid password" });
                        }
                    }
                    else
                    {
                        return Unauthorized(new { message = "Account is not active" });
                    }
                }
                else
                {
                    return NotFound(new { message = "User not found" });
                }
            }
            else if (usertype.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
            {
                var result = apiDbContext.Doctors.Where(p => p.Email.Equals(username)).FirstOrDefault();

                if (result != null)
                {
                    if (result.Account_Status.Equals("active", StringComparison.OrdinalIgnoreCase))
                    {
                        if (result.Password.Equals(password))
                        {
                            return Ok(new { message = "Authentication successful", doctorId = result.Doctor_Id });
                        }
                        else
                        {
                            return Unauthorized(new { message = "Invalid password" });
                        }
                    }
                    else
                    {
                        return Unauthorized(new { message = "Account is not active" });
                    }
                }
                else
                {
                    return NotFound(new { message = "User not found" });
                }
            }
            
            else if(usertype.Equals("Hospital",StringComparison.OrdinalIgnoreCase))
            {
                var result = apiDbContext.Hospitals.Where(p => p.HospitalEmail.Equals("contact@childrenshospital.org")).FirstOrDefault();
                if (result != null)
                {
                    if (result.AccountStatus.Equals("active", StringComparison.OrdinalIgnoreCase))
                    {
                        if (result.HospitalAccountPassword.Equals(password))
                        {
                            return Ok(new { message = "Authentication successful", hospitalId = result.HospitalId });
                        }
                        else
                        {
                            return Unauthorized(new { message = "Invalid password" });
                        }
                    }
                    else
                    {
                        return Unauthorized(new { message = "Account is not active" });
                    }
                }
                else
                {
                    return NotFound(new { message = "User not found" });
                }
            }

            else if (usertype.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                if (username.Equals("admin.ihhe@gmail.com") &&
                    password.Equals("Admin@IHHE"))
                {
                    return Ok(new { message = "Authentication successful", role = "Admin" });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }
            }

            else
            {
                return BadRequest(new { message = "Invalid user type" });
            }
            
        }

    }
}
