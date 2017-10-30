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
                    message.From.Add(new MailboxAddress(Configuration["Title"], Configuration["FromEmail"])); //mail title and mail from(Email)
                    message.To.Add(new MailboxAddress(people.Email)); //mail to(client)
                    message.Subject = Configuration["SubjectForEmailReset"]; //mail subject
                    var bodyBuilder = new BodyBuilder();
                    //body of the mail
                    bodyBuilder.HtmlBody = "Click here to join project-  http://localhost:4200/app-register/" + people.ProjectId; //link sent in mail
                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
			//required field for email
                        client.Connect(Configuration["Domain"], 587, false);
                        client.Authenticate(Configuration["FromEmail"], Configuration["Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                 
            
        }
    }
}
