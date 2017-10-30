using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyNeo4j.Service
{
    public interface IinviteMembersService
    {
        void EmailForInvitation(InvitePeople people);
    }

    public class InviteMembersService : IinviteMembersService
    {
       

        public InviteMembersService()
        {
            var builder = new ConfigurationBuilder() //config file for the email method
           .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();// config file for email id
        }
        public IConfigurationRoot Configuration { get; }



        public void EmailForInvitation(InvitePeople people)
        {
            
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(Configuration["Title"], Configuration["FromEmail"]));
                    message.To.Add(new MailboxAddress(people.Email));
                    message.Subject = Configuration["SubjectForEmailReset"];
                    var bodyBuilder = new BodyBuilder();
                    //body of the mail
                    bodyBuilder.HtmlBody = "Click here to reset your password-  http://localhost:4200/app-register/" + people.ProjectId;
                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(Configuration["Domain"], 587, false);
                        client.Authenticate(Configuration["FromEmail"], Configuration["Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                 
            
        }
    }
}
