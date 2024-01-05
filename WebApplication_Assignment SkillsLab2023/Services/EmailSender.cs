using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Services
{
    public static class EmailSender
    {
        public static async Task<bool> SendEmailAsync(string Subject, string Body, string Email)
        {
            string senderEmail = "UniHub@ceridian.com";
            var smtpClient = new SmtpClient("relay.ceridian.com")
            {
                Port = 25,
                EnableSsl = true,
                UseDefaultCredentials = true,
            };

            var mailMessage = new MailMessage(senderEmail, Email)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };

            try
            {
                Task.Run(() => { smtpClient.Send(mailMessage); }).ConfigureAwait(false);
#pragma warning restore CS4014
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}