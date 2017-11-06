using AgProMa.Repository;
using AgProMa.Services;
using ForgetPassword.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNeo4j.Hubs;
using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Service;

namespace MyNeo4j
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //used to create database on this connection link
            services.AddDbContext<Neo4jDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProjectMasterRepo, ProjectMasterRepo>();
            services.AddScoped<IProjectMasterService, ProjectMasterService>();
            services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
            services.AddScoped<IProjectMemberService, ProjectMemberService>();
            services.AddSignalR();
            services.AddScoped<IforgetPassword, forgetPassword>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ISignUpRepository, SignUpRepository>();
            services.AddScoped<IinviteMembersService, InviteMembersService>();
            services.AddScoped<IInviteRepository, InviteRepository>();
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
            services.AddScoped<ITaskBacklogReposiory, TaskBacklogRepository>();
            services.AddScoped<ITaskBacklogService, TaskBacklogService>();
            services.AddSingleton(Configuration);
            // Add framework services.
            ConfigureJwtAuthService(Configuration,services);
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           // services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseSignalR(routes =>
            {
               routes.MapHub<EpicHub>("epichub");
               routes.MapHub<SprintBacklogHub>("sprint");
               routes.MapHub<ReleasePlanHub>("releaseplan");
               routes.MapHub<TeamHub>("teamhub");
               routes.MapHub<TaskBacklogHub>("taskbacklog");
	           routes.MapHub<BacklogHub>("backlog");
                routes.MapHub<TaskHub>("taskhub");
            });
            
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
