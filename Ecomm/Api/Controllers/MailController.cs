using Application.DataTransfer.Mail;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : Controller
    {
        [HttpPost] 
        public IActionResult Post([FromBody] SendMailDto dto)
        {
            string from = "projekataspaleksa@outlook.com"; 
            MailMessage message = new MailMessage(from, dto.To);

            string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);  
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("projekataspaleksa@outlook.com", "aleksacar123"); 
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Ok("we send the mail thank you veri mach");
        }
    }
}
