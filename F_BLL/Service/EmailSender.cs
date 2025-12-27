using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace F_BLL.Service
{

    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using var client = new SmtpClient("smtp.gmail.com", 465)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        "ansnzal908@gmail.com",
                        "oeyqvgvfqefrmf"
                    )
                };

                var message = new MailMessage
                {
                    From = new MailAddress("ansnzal908@gmail.com", "KASHOP"),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                message.To.Add(email);

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // مهم: حتى تعرف الخطأ الحقيقي
                throw new Exception("Email sending failed: " + ex.Message);
            }
        }
    }

}

