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
        List<AvailTeamMember> getAvailableMember(int projectId);
        void addTeam(TeamMaster team);
        void addMembers(TeamMember member);
        void deleteMember(int id);
        void UpdateConnectionId(string connectionid, int memberid);
    }
    public class TeamService : ITeamService
    {
        private Neo4jDbContext _dbcon;
        private ITeamRepo _teamRepo;
        //reomve dbcon and use existing method in others repo
        public TeamService(ITeamRepo _teamRepo, Neo4jDbContext dbcon)
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

        public List<AvailTeamMember> getAvailableMember(int projectId)
        {
            List<AvailTeamMember> availteam = new List<AvailTeamMember>();
            List<ProjectMember> promem = _teamRepo.getProjectMember(projectId);
            List<TeamMember> finalmemlist = new List<TeamMember>();
            List<TeamMember> teammem = new List<TeamMember>();
            List<TeamMaster> teams = getTeam(projectId);
            foreach (TeamMaster tm in teams)
            {
                teammem = _teamRepo.getTeamMember(tm.TeamId);
                 foreach(TeamMember temem in teammem)
                {
                    finalmemlist.Add(temem);
                }
            }
            foreach (ProjectMember pm in promem)
            {
                Master master = _dbcon.Pmaster.FirstOrDefault(p => p.Id == pm.MemberId);
                AvailTeamMember avail = new AvailTeamMember();
                avail.MemberId = master.Id;
                avail.MemberName = master.FirstName + ' ' + master.LastName;
                foreach (TeamMember tmm in finalmemlist)
                {
                    if (pm.MemberId == tmm.MemberId)
                    {
                        avail.TeamId = tmm.TeamId;
                        avail.Id = tmm.id;
                        break;
                    }
                    else
                    {
                        avail.TeamId = 0;
                        avail.Id = 0;
                    }               
                }
                availteam.Add(avail);
            }
            return availteam;
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

      

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _teamRepo.UpdateConnectionId(connectionid, memberid);
        }

    }
}
