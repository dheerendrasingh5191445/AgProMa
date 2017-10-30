using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgetPassword.service
{
    public interface IforgetPassword
    {
        bool EmailForResetPassword(string email);
    }

    public class forgetPassword : IforgetPassword
    {
        private IMasterRepository _repo;
       
        public forgetPassword(IMasterRepository repo)
        {
            _repo = repo;
            var builder = new ConfigurationBuilder() //config file for the email method
           .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();// config file for email id
        }
        public IConfigurationRoot Configuration { get; }



        public bool EmailForResetPassword(string email)
        {
            List<Master> master = _repo.getAll();
            foreach (Master mast in master)
            {
                if (mast.Email == email)
                {

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(Configuration["Title"], Configuration["FromEmail"])); //Mail title and mail from(Email)
                    message.To.Add(new MailboxAddress(email)); // Mail to(Email)
                    message.Subject = Configuration["SubjectForEmailReset"]; //Mail's Subject
                    var bodyBuilder = new BodyBuilder();
                    //body of the mail
                    bodyBuilder.HtmlBody = "Click here to reset your password-  http://localhost:4200/app-register-user-with-new-password/" + 1;
                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
			//details required for mail
                        client.Connect(Configuration["Domain"], 587, false);
                        client.Authenticate(Configuration["FromEmail"], Configuration["Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    return true;
                }
            }
            return false;

        }
    }
}
