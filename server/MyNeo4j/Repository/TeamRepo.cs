using MyNeo4j.model;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface ITeamRepo
    {
        List<TeamMaster> getTeam();
        List<ProjectMember> getProjectMember(int projectId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<TeamMember> getTeamMember(int teamId);
        void addTeam(TeamMaster team);
        void addMembers(TeamMember member);
        void deleteMember(int id);
    }
    public class TeamRepo : ITeamRepo
    {
        private Neo4jDbContext _neo4JDbContext;
        public TeamRepo(Neo4jDbContext _neo4JDbContext)
        {
            this._neo4JDbContext = _neo4JDbContext;
        }
        public void addMembers(TeamMember member)
        {
            _neo4JDbContext.Teammemeber.Add(member);
            _neo4JDbContext.SaveChanges();
        }

        public void addTeam(TeamMaster team)
        {
            _neo4JDbContext.Teammaster.Add(team);
            _neo4JDbContext.SaveChanges();
        }

        public void deleteMember(int id)
        {
            TeamMember member = _neo4JDbContext.Teammemeber.FirstOrDefault(m => m.id == id);
           _neo4JDbContext.Teammemeber.Remove(member);
            _neo4JDbContext.SaveChanges();
        }

        public List<ProjectMember> getProjectMember(int id)
        {
            return _neo4JDbContext.Projectmember.Where(p => p.ProjectId == id).ToList();
        }

        public List<TeamMaster> getTeam()
        {
            return _neo4JDbContext.Teammaster.ToList();
        }

        public List<TeamMember> getTeamMember(int id)
        {
            return _neo4JDbContext.Teammemeber.Where(p => p.TeamId == id).ToList();
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _neo4JDbContext.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.team;
            _neo4JDbContext.SaveChanges();
        }
    }
}
