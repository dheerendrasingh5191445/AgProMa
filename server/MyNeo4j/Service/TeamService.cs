﻿using MyNeo4j.model;
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
        List<TeamMaster> GetTeam(int projectId);
        List<AvailTeamMember> GetAvailableMember(int projectId);
        void AddTeam(TeamMaster team);
        void AddMembers(TeamMember member);
        void DeleteMember(int id);
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
        //this method will add members to a team
        public void AddMembers(TeamMember member)
        {
            _teamRepo.AddMembers(member);
        }

        //this method will add team to a project
        public void AddTeam(TeamMaster team)
        {
            _teamRepo.AddTeam(team);
        }

        //this method will delete member from a team
        public void DeleteMember(int id)
        {
            _teamRepo.DeleteMember(id);
        }

        //this method will return available members in a project
        public List<AvailTeamMember> GetAvailableMember(int projectId)
        {
            List<AvailTeamMember> availteam = new List<AvailTeamMember>();
            List<ProjectMember> promem = _teamRepo.GetProjectMember(projectId);
            List<TeamMember> finalmemlist = new List<TeamMember>();
            List<TeamMember> teammem = new List<TeamMember>();
            List<TeamMaster> teams = GetTeam(projectId);
            foreach (TeamMaster tm in teams)
            {
                teammem = _teamRepo.GetTeamMember(tm.TeamId);
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
                        avail.Id = tmm.Id;
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

        //this method will return teams of a particular project
        public List<TeamMaster> GetTeam(int projectId)
        {
            List<TeamMaster> teamMaster = _teamRepo.GetTeam();
            List<TeamMaster> teamlistbyproject = new List<TeamMaster>();
            foreach (TeamMaster item in teamMaster)
            {
                if (item.ProjectId == projectId)
                {
                    teamlistbyproject.Add(item);
                }
            }
            return teamlistbyproject;
        }
        //this method will update connection Id for particular member 
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _teamRepo.UpdateConnectionId(connectionid, memberid);
        }

    }
}
