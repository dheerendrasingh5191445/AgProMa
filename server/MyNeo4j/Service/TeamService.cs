using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface ITeamService
    {
        List<TeamMaster> getTeam(int projectId);
        List<AvailableMember> getAvailableMember(int projectId);
        List<AvailTeamMember> getTeamMember(int teamId);
        void addTeam(TeamMaster team);
        void addMembers(TeamMember member);
        void deleteMember(int id);
    }
    public class TeamService : ITeamService
    {
        private Neo4jDbContext _dbcon;
        private ITeamRepo _teamRepo;
        public TeamService(ITeamRepo _teamRepo, Neo4jDbContext dbcon)//reomve dbcon and use existing method in others repo
        {
            this._teamRepo = _teamRepo;
            _dbcon = dbcon;
        }
        public void addMembers(TeamMember member)
        {
            _teamRepo.addMembers(member);
        }

        public void addTeam(TeamMaster team)
        {
            _teamRepo.addTeam(team);
        }

        public void deleteMember(int id)
        {
            _teamRepo.deleteMember(id);
        }

        public List<TeamMaster> getTeam(int projectId)
        {
             List<TeamMaster> teamMaster= _teamRepo.getTeam();
             List<TeamMaster> teamlistbyproject = new List<TeamMaster>();
             foreach (TeamMaster item in teamMaster)
            {
                if(item.ProjectId == projectId)
                {
                    teamlistbyproject.Add(item);
                }
            }
            return teamlistbyproject;
        }

        public List<AvailTeamMember> getTeamMember(int teamId)
        {
            List<AvailTeamMember> availteam = new List<AvailTeamMember>();
            List<TeamMember> teammas = _teamRepo.getTeamMember();
            foreach(TeamMember tm in teammas)
            {
                if(tm.TeamId == teamId)
                {
                    Master master = _dbcon.Pmaster.FirstOrDefault(p => p.Id == tm.MemberId);
                    AvailTeamMember avail = new AvailTeamMember();
                    avail.MemberId = master.Id;
                    avail.MemberName = master.FirstName +' '+master.LastName;
                    avail.TeamId = tm.TeamId;
                    avail.Id = tm.id;
                    availteam.Add(avail);
                }
            }
            return availteam;

        }

        public List<AvailableMember> getAvailableMember(int projectId)
        {
            List<TeamMember> teammas = _teamRepo.getTeamMember();
            List<AvailableMember> availteam = new List<AvailableMember>();
            List<ProjectMember> promas = _teamRepo.getProjectMember();
            foreach (ProjectMember tm in promas)
            {
                if (tm.ProjectId == projectId)
                {
                    int count = 0;
                    foreach (TeamMember t in teammas)
                    {                   
                       if(tm.MemberId == t.MemberId)
                        { count++; break; }
                    }
                    if(count == 0)
                    {
                        Master master = _dbcon.Pmaster.FirstOrDefault(p => p.Id == tm.MemberId);
                        AvailableMember avail = new AvailableMember();
                        avail.MemberId = master.Id;
                        avail.MemberName = master.FirstName + ' ' + master.LastName;

                        availteam.Add(avail);
                    }
                }
            }
            return availteam;
        }
    }
}
