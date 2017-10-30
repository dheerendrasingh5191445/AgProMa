using Microsoft.EntityFrameworkCore;

namespace MyNeo4j.model
{
    public class Neo4jDbContext:DbContext
    {//this model is database model which is used to make the table in my database
            public Neo4jDbContext(DbContextOptions options) : base (options)
            {

            }

            public DbSet<ActivitiesHappened> Activityhappened { get; set; }
            public DbSet<ChecklistBacklog> Checklistbl  { get; set; }
            public DbSet<CommentLog> Commentlog { get; set; }
            public DbSet<Master> Pmaster  { get; set; }
            public DbSet<ProductBacklog> Productbl { get; set; }
            public DbSet<ProjectMaster> ProjectM { get; set; }
            public DbSet<ReleasePlanMaster> Releasepl { get; set; }
            public DbSet<SocialSignupMaster> Socialsm { get; set; }
            public DbSet<SprintBacklog> Sprintbl { get; set; }
            public DbSet<TaskBacklog> Taaskbl { get; set; }
            public DbSet<TeamMaster> Teammaster { get; set; }
            public DbSet<TeamMember> Teammemeber { get; set; }
            public DbSet<ProjectMember> Projectmember { get; set; }
            public DbSet<SignalRMaster> SignalRDb { get; set; }
            public DbSet<EpicMaster> EpicDb { get; set; }
    }
}

