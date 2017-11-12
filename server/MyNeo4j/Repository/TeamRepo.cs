using MyNeo4j.model;
using System.Collections.Generic;
using System.Linq;

namespace MyNeo4j.Repository
{
    public interface ITeamRepo
    {
        List<TeamMaster> GetTeam();
        Master Master(int Id);
        List<ProjectMember> GetProjectMember(int projectId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<TeamMember> GetTeamMember(int teamId);
        void AddTeam(TeamMaster team);
        void AddMembers(TeamMember member);
        void DeleteMember(int id);
    }
    public class TeamRepo : ITeamRepo
    {
        private Neo4jDbContext _neo4JDbContext;
        public TeamRepo(Neo4jDbContext _neo4JDbContext)
        {
            this._neo4JDbContext = _neo4JDbContext;
        }
        //this method will add members to a team
        public void AddMembers(TeamMember member)
        {
            _neo4JDbContext.Teammemeber.Add(member);
            _neo4JDbContext.SaveChanges();
        }

        //this method will add team to a project
        public void AddTeam(TeamMaster team)
        {
            _neo4JDbContext.Teammaster.Add(team);
            _neo4JDbContext.SaveChanges();
        }

        //this method will delete member from a team
        public void DeleteMember(int id)
        {
            TeamMember member = _neo4JDbContext.Teammemeber.FirstOrDefault(m => m.Id == id);
           _neo4JDbContext.Teammemeber.Remove(member);
            _neo4JDbContext.SaveChanges();
        }

        //this method will return project members
        public List<ProjectMember> GetProjectMember(int id)
        {
            return _neo4JDbContext.Projectmember.Where(p => p.ProjectId == id).ToList();
        }

        //this method will return teams 
        public List<TeamMaster> GetTeam()
        {
            return _neo4JDbContext.Teammaster.ToList();
        }

        //this method will return team members of a particular team
        public List<TeamMember> GetTeamMember(int id)
        {
            return _neo4JDbContext.Teammemeber.Where(p => p.TeamId == id).ToList();
        }

        public Master Master(int Id)
        {
            return _neo4JDbContext.Pmaster.FirstOrDefault(p => p.Id == Id);
        }

        //this method will update connection id of a member
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _neo4JDbContext.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.team;
            _neo4JDbContext.SaveChanges();
        }
    }
}
