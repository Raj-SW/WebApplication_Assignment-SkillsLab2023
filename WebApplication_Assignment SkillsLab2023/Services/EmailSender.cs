using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Services
{
    public static class EmailSender
    {
        public static async Task<string> SendEmailAsync(string Subject, string Body, string recipientEmail)
        {
            string senderEmail = "Raj.Seetohul@ceridian.com";
            var smtpClent = new SmtpClient("relay.ceridian.com")
            {
                Port = 25,
                EnableSsl = true,
                UseDefaultCredentials = true,
            };

            var mailMessage = new MailMessage(senderEmail, recipientEmail)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };

            try
            {
                await smtpClent.SendMailAsync(mailMessage);
                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}