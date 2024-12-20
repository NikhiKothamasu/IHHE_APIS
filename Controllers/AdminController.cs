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
                var results = apiDbContext.Patients.Select(p => new { p.PatientId, p.PatientName,p.Account_Status }).ToList();
                return Ok(results);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpDelete("DeletePatientPermanently")]
        public IActionResult deletePatient([FromQuery] Guid PatientId)
        {
            try
            {
                var appointments = apiDbContext.Appointments.Where(a => a.PatientId.Equals(PatientId)).ToList();
                var appointmentIds = appointments.Select(a => a.AppointmentId).ToList();
                var appointmentDatas = apiDbContext.AppointmentDatas.Where(ad => appointmentIds.Contains(ad.AppointmentId)).ToList();
                if (appointments.Any())
                {
                    apiDbContext.Appointments.RemoveRange(appointments);
                }
                if (appointmentDatas.Any())
                {
                    apiDbContext.AppointmentDatas.RemoveRange(appointmentDatas);
                }
                var patient = apiDbContext.Patients.Where(p => p.PatientId.Equals(PatientId)).FirstOrDefault();
                apiDbContext.Patients.RemoveRange(patient);
                apiDbContext.SaveChanges();
                return Ok(new { message = "Patient Details deleted successfully" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPut("UpdateAccountStatus")]
        public IActionResult updateAccountStatus([FromQuery] Guid PatientId)
        {
            try
            {
                var patient = apiDbContext.Patients.FirstOrDefault(p => p.PatientId.Equals(PatientId));
                patient.Account_Status = "Active";
                apiDbContext.SaveChanges();
                return Ok(new { message = "Account Status Changed Successfully" });
            }
            catch(Exception ex)
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
            catch (Exception ex) {
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
                SendEmailTicketRasing(insert_data.UserEmail, insert_data.TicketId, insert_data.Issue,patient_name);
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
