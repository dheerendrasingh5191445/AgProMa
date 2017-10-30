using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNeo4j.model;
using Microsoft.EntityFrameworkCore;
using MyNeo4j.Repository;
using MyNeo4j.Service;
using MyNeo4j.Hubs;
using ForgetPassword.service;
using AgProMa.Services;
using AgProMa.Repository;

namespace MyNeo4j
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //used to create database on this connection link
            services.AddDbContext<Neo4jDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProjectMasterRepo, ProjectMasterRepo>();
            services.AddScoped<IProjectMasterService, ProjectMasterService>();
            services.AddSignalR();
            services.AddScoped<IforgetPassword, forgetPassword>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ISignUpRepository, SignUpRepository>();
            services.AddScoped<IinviteMembersService, InviteMembersService>();
            services.AddScoped<IProjectMemberService, ProjectMemberService>();
            services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
            services.AddScoped<ITeamRepo, TeamRepo>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IEpicServices, EpicService>();
            services.AddScoped<IEpicRepository, EpicRepository>();
            services.AddScoped<IBacklogServices, BacklogService>();
            services.AddScoped<IBacklogRepository, BacklogRepository>();
            services.AddScoped<IReleasePlanRepo,ReleasePlanRepo>();
            services.AddScoped<IReleasePlanService,ReleasePlanService>();
            services.AddScoped<ISprintRepository, SprintRepository>();
            services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<ICheckListRepository, ChecklistRepository>();
            services.AddScoped<ICheckListService, ChecklistService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskServices, TaskService>();
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ProjectMasterHub>("promaster");
            });
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseMvc();
        }
    }
}
