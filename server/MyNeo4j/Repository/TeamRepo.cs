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
        List<ProjectMember> getProjectMember();
        List<TeamMember> getTeamMember();
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

        public List<ProjectMember> getProjectMember()
        {
            return _neo4JDbContext.Projectmember.ToList();
        }

        public List<TeamMaster> getTeam()
        {
            return _neo4JDbContext.Teammaster.ToList();
        }

        public List<TeamMember> getTeamMember()
        {
            return _neo4JDbContext.Teammemeber.ToList();
        }
    }
}
