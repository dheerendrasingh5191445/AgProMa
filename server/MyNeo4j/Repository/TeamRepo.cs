using MyNeo4j.model;
using System.Collections.Generic;
using System.Linq;

namespace MyNeo4j.Repository
{
    public interface ITeamRepo
    {
        List<TeamMaster> GetTeam();
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

        //this method will add member to a team
        public void AddMembers(TeamMember member)
        {
            _neo4JDbContext.Teammemeber.Add(member);
            _neo4JDbContext.SaveChanges();
        }

        //this method  will add team to a project
        public void AddTeam(TeamMaster team)
        {
            _neo4JDbContext.Teammaster.Add(team);
            _neo4JDbContext.SaveChanges();
        }

        //this method will delete a a  team member
        public void DeleteMember(int id)
        {
            TeamMember member = _neo4JDbContext.Teammemeber.FirstOrDefault(m => m.Id == id);
           _neo4JDbContext.Teammemeber.Remove(member);
            _neo4JDbContext.SaveChanges();
        }

        //this method  will return members of a particular project
        public List<ProjectMember> GetProjectMember(int id)
        {
            return _neo4JDbContext.Projectmember.Where(p => p.ProjectId == id).ToList();
        }

        //this method will return teams 
        public List<TeamMaster> GetTeam()
        {
            return _neo4JDbContext.Teammaster.ToList();
        }

        //this method will return members of a team
        public List<TeamMember> GetTeamMember(int id)
        {
            return _neo4JDbContext.Teammemeber.Where(p => p.TeamId == id).ToList();
        }

        //this method will update connection id of a member
        public void UpdateConnectionId(string connectionId, int memberId)
        {
            SignalRMaster signalr = _neo4JDbContext.SignalRDb.FirstOrDefault(m => m.MemberId == memberId);
            signalr.ConnectionId = connectionId;
            signalr.HubCode = HubCode.team;
            _neo4JDbContext.SaveChanges();
        }
    }
}
