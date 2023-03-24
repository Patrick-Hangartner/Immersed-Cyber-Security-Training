using Immersed.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Immersed.Models.Requests;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Options;
using Immersed.Models.AppSettings;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Immersed.Services.Interfaces;
using SendGrid.Helpers.Mail.Model;
using Amazon.Runtime.Internal;
using Immersed.Models.Requests.InviteMembers;
using Immersed.Models.Domain.Messages;
using Immersed.Models.Enums;

namespace Immersed.Services
{
    public class EmailsService : IEmailsService
    {
        private AppKeys _appKeys;
        private readonly IWebHostEnvironment _webHostEnvironment;
         IDataProvider _data = null;
        private IUserService _userService = null;

        public EmailsService(IDataProvider data, IOptions<AppKeys> appKeys, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            _appKeys = appKeys.Value;
            _data = data;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }
         
        private async Task SendPhishing(SendGridMessage notice)
        {
            var client = new SendGridClient(_appKeys.SendGridKey);
            var response = await client.SendEmailAsync(notice);
   
        }
  
  
  public async void WelcomeEmail()
        {
           
            var from = new EmailAddress("fakeEmail@dispostable.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("fakeEmail@dispostable.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = GetTemplate();
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent );
            await SendEmail(msg);
        }
        
          public string GetTemplate()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = "";
            path = Path.Combine(webRootPath, "EmailTemplates", "WelcomeTemplate.html");

            string template = File.ReadAllText(path);
            return template;
        }

        public async void PhishingEmail(string token, PhishingAddRequest model)
        {
            var fromEmail = new EmailAddress() 
            { 
                Email = model.FromEmail,
                Name = model.FromName
            };
        

            var toEmail = new EmailAddress()
            {
                Email = model.ToEmail,
                Name = model.ToName
            };

            var htmlContent = PhishingTemplate(token, model.ToEmail);
            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, model.Subject, model.Body, htmlContent);
            await SendEmail(msg);
           
        }

        public string PhishingTemplate(string token, string email)
        {
            int tokenTypeId = (int)TokenType.TrainingEvent;
            string tokenType = tokenTypeId.ToString();
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = "";
            path = Path.Combine(webRootPath, "EmailTemplates", "PhishingEmail.html");
            string domain = _appKeys.Domain;
            string phishing = File.ReadAllText(path).Replace("{{domain}}", domain).Replace("{{token}}", token).Replace("{{email}}", email).Replace("{{tokenTypeId}}", tokenType);
            return phishing;
        }


    }
} 
