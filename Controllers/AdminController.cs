using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Apis.Data;
using Project_Apis.Models;
using System.Net.Mail;
using System.Net;
using Project_Apis.ViewModel;

namespace Project_Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApiDbContext apiDbContext;
        private readonly ILogger<HomeController> _logger;
        public AdminController(ApiDbContext apiDbContext, ILogger<HomeController> logger)
        {
            this.apiDbContext = apiDbContext;
            this._logger = logger;
        }

        // Patients Related APIS

        [HttpGet("PatientsList")]
        public IActionResult patientsList()
        {
            try
            {
                var results = apiDbContext.Patients.Select(p => new { p.PatientId, p.PatientName, p.Account_Status }).ToList();
                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpGet("DoctorsList")]
        public IActionResult doctorsList()
        {
            try
            {
                var results = apiDbContext.Doctors.Select(p => new { p.Doctor_Id, p.Name, p.Account_Status }).ToList();
                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpGet("HospitalList")]
        public IActionResult hospitalList()
        {
            try
            {
                var results = apiDbContext.Hospitals.Select(p => new { p.HospitalId, p.HospitalName, p.AccountStatus }).ToList();
                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }



        //[HttpDelete("DeletePatientPermanently")]
        //public IActionResult deletePatient([FromQuery] int PatientId)
        //{
        //    try
        //    {
        //        var appointments = apiDbContext.Appointments.Where(a => a.PatientId == PatientId).ToList();
        //        var appointmentIds = appointments.Select(a => a.AppointmentId).ToList();
        //        var appointmentDatas = apiDbContext.AppointmentDatas.Where(ad => appointmentIds.Contains(ad.AppointmentId)).ToList();
        //        if (appointments.Any())
        //        {
        //            apiDbContext.Appointments.RemoveRange(appointments);
        //        }
        //        if (appointmentDatas.Any())
        //        {
        //            apiDbContext.AppointmentDatas.RemoveRange(appointmentDatas);
        //        }
        //        var patient = apiDbContext.Patients.Where(p => p.PatientId.Equals(PatientId)).FirstOrDefault();
        //        apiDbContext.Patients.RemoveRange(patient);
        //        apiDbContext.SaveChanges();
        //        return Ok(new { message = "Patient Details deleted successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        //    }
        //}

        [HttpPut("UpdateAccountStatus")]
        public IActionResult UpdateAccountStatus([FromQuery] string usertype, [FromQuery] int? PatientId,[FromQuery] Guid? Id)
        {
            try
            {
                if (usertype.Equals("Patient", StringComparison.OrdinalIgnoreCase))
                {
                    if (PatientId.HasValue)
                    {
                        var patient = apiDbContext.Patients
                            .FirstOrDefault(p => p.PatientId == PatientId);

                        if (patient != null)
                        {
                            patient.Account_Status = "Active";
                            apiDbContext.SaveChanges();
                            return Ok(new { message = "Patient account status updated successfully" });
                        }
                        return NotFound(new { message = "Patient not found" });
                    }
                    return BadRequest(new { message = "PatientId is required for Patient usertype" });
                }
                else if (usertype.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                {
                    if (Id.HasValue)
                    {
                        var doctor = apiDbContext.Doctors
                            .FirstOrDefault(d => d.Doctor_Id.Equals(Id.Value));

                        if (doctor != null)
                        {
                            
                            doctor.Account_Status = "Active"; 
                            apiDbContext.SaveChanges();
                            return Ok(new { message = "Doctor account status updated successfully" });
                        }
                        return NotFound(new { message = "Doctor not found" });
                    }
                    return BadRequest(new { message = "DoctorId is required for Doctor usertype" });
                }
                else if (usertype.Equals("Hospital", StringComparison.OrdinalIgnoreCase))
                {
                    if (Id.HasValue)
                    {
                        var hospital = apiDbContext.Hospitals
                            .FirstOrDefault(h => h.HospitalId.Equals(Id.Value));

                        if (hospital != null)
                        {
                            
                            hospital.AccountStatus = "Active"; 
                            apiDbContext.SaveChanges();
                            return Ok(new { message = "Hospital account status updated successfully" });
                        }
                        return NotFound(new { message = "Hospital not found" });
                    }
                    return BadRequest(new { message = "HospitalId is required for Hospital usertype" });
                }
                else
                {
                    return BadRequest(new { message = "Invalid usertype" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }


        [HttpGet("GetPassword")]
        public IActionResult GetPassword([FromQuery] string usertype, [FromQuery] int? PatientId, [FromQuery] Guid? Id)
        {
            try
            {
                if (usertype.Equals("Patient", StringComparison.OrdinalIgnoreCase))
                {
                    if (PatientId.HasValue)
                    {
                        var result = apiDbContext.Patients
                            .Where(p => p.PatientId == PatientId.Value)
                            .Select(p => p.Password)
                            .FirstOrDefault();

                        if (result != null)
                        {
                            return Ok(result);
                        }
                        return NotFound(new { message = "Patient not found" });
                    }
                    return BadRequest(new { message = "PatientId is required for Patient usertype" });
                }
                else if (usertype.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                {
                    if (Id.HasValue)
                    {
                        var result = apiDbContext.Doctors
                            .Where(d => d.Doctor_Id.Equals(Id.Value)).Select(d =>d.Password)
                            .FirstOrDefault();

                        if (result != null)
                        {
                            return Ok(result);
                        }
                        return NotFound(new { message = "Doctor not found" });
                    }
                    return BadRequest(new { message = "DoctorId is required for Doctor usertype" });
                }
                else if (usertype.Equals("Hospital", StringComparison.OrdinalIgnoreCase))
                {
                    if (Id.HasValue)
                    {
                        var result = apiDbContext.Hospitals
                            .Where(h => h.HospitalId.Equals(Id.Value))
                            .Select(h => h.HospitalAccountPassword)
                            .FirstOrDefault();

                        if (result != null)
                        {
                            return Ok(result);
                        }
                        return NotFound(new { message = "Hospital not found" });
                    }
                    return BadRequest(new { message = "HospitalId is required for Hospital usertype" });
                }
                else
                {
                    return BadRequest(new { message = "Invalid usertype" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        //APPOINTMENT RELATED APIS

        [HttpGet("AppointmentsFetch")]
        public IActionResult getAppointments()
        {
            try
            {
                var appointments = apiDbContext.Appointments.ToList();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        // Appointment Status Change

        [HttpPut("ApointmentStatusModification")]
        public IActionResult modifyAppointmentStatus([FromQuery] Guid AppointmentId, [FromQuery] string Status)
        {
            try
            {
                var appointment = apiDbContext.Appointments.SingleOrDefault(a => a.AppointmentId.Equals(AppointmentId));
                if (appointment == null)
                {
                    return NotFound(new { message = "Appointment not found" });
                }
                appointment.Status = Status;
                apiDbContext.SaveChanges();
                return Ok(new { message = "Appointment Status Changed Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }


        //Ticket Creation
        [HttpPost("RaiseTicket")]
        public IActionResult AddMessage(TicketsView add_data)
        {
            var insert_data = new Tickets
            {
                TicketId = Guid.NewGuid(),
                UserType = add_data.UserType,
                UserEmail = add_data.UserEmail,
                UserId = add_data.UserId,
                Issue = add_data.Issue,
                Status = "Pending"
            };
            apiDbContext.Add(insert_data);
            apiDbContext.SaveChanges();

            if (add_data.UserType.Equals("Doctor", StringComparison.Ordinal))
            {
                var doctor_name = apiDbContext.Doctors.Where(p => p.Doctor_Id.Equals(add_data.UserId))
                                                       .Select(p => p.Name)
                                                       .FirstOrDefault();

                SendEmailTicketRasing(insert_data.UserEmail, insert_data.TicketId, insert_data.Issue, doctor_name);
                return Ok(insert_data);
            }
            else if (add_data.UserType.Equals("Patient", StringComparison.Ordinal))
            {

                var patient_name = apiDbContext.Patients.Where(p => p.PatientId == Convert.ToInt32(add_data.UserId))
                                                       .Select(p => p.PatientName)
                                                       .FirstOrDefault();
                SendEmailTicketRasing(insert_data.UserEmail, insert_data.TicketId, insert_data.Issue, patient_name);
            }
            else
            {
                var hospital_name = apiDbContext.Doctors.Where(p => p.HospitalId.Equals(add_data.UserId))
                                                         .Select(p => p.Name)
                                                         .FirstOrDefault();
                SendEmailTicketRasing(insert_data.UserEmail, insert_data.TicketId, insert_data.Issue, hospital_name);
            }
            return Ok(insert_data);
        }


        private void SendEmailTicketRasing(string recipientEmail, Guid ticketId, string issue, string recipientName)
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
                    Subject = "Ticket Raised for Your Reported Issue",
                    Body = $"Dear User,\n\n" +
                   "Thank you for reaching out to IHHE Hospitals. We have received your report regarding the following issue:\n\n" +
                   $"Issue: {issue}\n\n" +
                   "A support ticket has been generated to address this matter, and our team is working to resolve it as soon as possible. Your ticket ID is: " +
                   $"{ticketId}\n\n" +
                   "We understand the importance of this issue and are prioritizing its resolution. You will receive further updates regarding the progress of your ticket.\n\n" +
                   "Thank you for your patience and cooperation.\n\n" +
                   "Best regards,\n" +
                   "IHHE Team\n\n" +
                   "(Note: This is an automated message. Please do not reply to this email.)",
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
    }
}
