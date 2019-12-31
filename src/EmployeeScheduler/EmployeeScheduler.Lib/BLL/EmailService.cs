using System;
using System.Net;
using System.Net.Mail;

namespace EmployeeScheduler.Lib.BLL
{
    public static class EmailService
    {
        // This clearly doesn't work, so I'm shelving the email feature for now.
        // Leaving the code for potential use later.
        private const string _fromAddress = "no-reply@nunya.com";
        private const string _smtpServer = "smtp.gmail.com";
        private const int _port = 587;

        public static void SendEmail(string message, string toAddress)
        {
            try
            {
                var email = new MailMessage();

                email.From = new MailAddress(_fromAddress);
                email.To.Add(new MailAddress(toAddress));
                email.Subject = "Test";
                email.IsBodyHtml = false;
                email.Body = message;

                var smtp = new SmtpClient();
                smtp.Port = _port;
                smtp.Host = _smtpServer;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(email);
            }
            catch (Exception)
            {

            }
        }
    }
}
