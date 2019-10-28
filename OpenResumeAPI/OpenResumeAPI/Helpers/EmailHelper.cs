using OpenResumeAPI.Models;
using System;
using System.Security.Cryptography;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using OpenResumeAPI.Helpers.Interfaces;

namespace OpenResumeAPI.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        IAppSettings appSettings;

        public EmailHelper(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string CreateToken(User user)
        {
            StringBuilder result = new StringBuilder();
            string input = $"{user.Name};{user.Email};{DateTime.Now.Ticks}";
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            for (int i = 0; i < data.Length; i++)
            {
                result.Append(data[i].ToString("x2"));
            }
            return result.ToString();
        }

        public void SendConfirmationEmail(User user)
        {          
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"<p>Please confirm your email cliking o the link below:</p>
                                      <br/>
                                      <a href='{appSettings.Home}emailconfirm.html?token={user.ConfirmationToken}'>Confirm</a>                                      
                                      <br/>
                                      <p>This link will be available for 24 hours.</p>
                                      <br/>
                                     <a href='{appSettings.Home}'>Open-Resume</a>";
            bodyBuilder.TextBody = $@"Please confirm your email cliking o the link below:

                                     {appSettings.Home}emailconfirm.html?token={user.ConfirmationToken}
                                     This link will be available for 24 hours.

                                     {appSettings.Home}";

            SendEmail(bodyBuilder, user);          
        }        

        public void SendResetEmail(User user)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"<p>Please reset your password cliking o the link below:</p>
                                      <br/>
                                      <a href='{appSettings.Home}passwordreset.html?token={user.ResetToken}'>Confirm</a>                                      
                                      <br/>
                                      <p>If you don't request this, please ignore.</p>
                                      <br/>
                                     <a href='{appSettings.Home}'>Open-Resume</a>";
            bodyBuilder.TextBody = $@"Please reset your password cliking o the link below:

                                     {appSettings.Home}passwordreset.html?token={user.ResetToken}
                                     If you don't request this, please ignore.

                                     {appSettings.Home}";

            SendEmail(bodyBuilder, user);
        }

        private void SendEmail(BodyBuilder body, User user)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(appSettings.From, appSettings.Email);
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress(user.Name, user.Email);
            message.To.Add(to);
            message.Subject = appSettings.Subject;
            message.Body = body.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect(appSettings.EmailServer, appSettings.EmailPort, false);
            client.Authenticate(appSettings.EmailUser, appSettings.EmailPassword);
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }

    }
}
