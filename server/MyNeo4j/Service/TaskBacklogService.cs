﻿using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface ITaskBacklogService
    {
        List<TaskBacklog> GetAllTask(int id);
        List<TeamMaster> GetByTeamId(int SprintId);
        List<AvailTeamMember> getTeamMember(int teamId);
        void UpdateTask(int memberID, int taskId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<AvailableMember> GetName(int id);
    }
    public class TaskBacklogService : ITaskBacklogService
    {
        ITaskBacklogReposiory _taskBacklog;
        public TaskBacklogService(ITaskBacklogReposiory taskBacklog)
        {
            _taskBacklog = taskBacklog;
        }

        public List<TeamMaster> GetByTeamId(int SprintId)
        {
            int proId = 0;
            List<SprintBacklog> allsprint = _taskBacklog.AllSprint();
            List<TeamMaster> teamMaster = _taskBacklog.getTeamName();
            List<TeamMaster> teamlistbyproject = new List<TeamMaster>();
            foreach (SprintBacklog spr in allsprint)
            {
                if(spr.SprintId == SprintId)
                {
                    proId = spr.ProjectId;
                    break;
                }
            }
            if (proId != 0)
            {
                foreach (TeamMaster item in teamMaster)
                {
                    if (item.ProjectId == proId)
                    {
                        teamlistbyproject.Add(item);
                    }
                }
                return teamlistbyproject;
            }
            return teamlistbyproject;
        }

        public List<AvailableMember> GetName(int id)
        {
            List<AvailableMember> availteam = new List<AvailableMember>();
            SprintBacklog sprint = _taskBacklog.GetProjectId(id);
            List<ProjectMember> projectmem = _taskBacklog.AllMember(sprint.ProjectId);
            foreach (ProjectMember pro in projectmem)
            {
                    Master master = _taskBacklog.Master(pro.MemberId);
                    AvailableMember avail = new AvailableMember();
                    avail.MemberId = master.Id;
                    avail.MemberName = master.FirstName + ' ' + master.LastName;
                    availteam.Add(avail);
            }
            return availteam;
        }

        public List<AvailTeamMember> getTeamMember(int teamId)
        {
            List<AvailTeamMember> availteam = new List<AvailTeamMember>();
            List<TeamMember> teammas = _taskBacklog.AllTeamMember();
            foreach (TeamMember tm in teammas)
            {
                if (tm.TeamId == teamId)
                {
                    Master master = _taskBacklog.Master(tm.MemberId);
                    AvailTeamMember avail = new AvailTeamMember();
                    avail.MemberId = master.Id;
                    avail.MemberName = master.FirstName + ' ' + master.LastName;
                    avail.TeamId = tm.TeamId;
                    avail.Id = tm.Id;
                    availteam.Add(avail);
                }
            }
            return availteam;
        }

        public void UpdateTask(int memberID, int taskId)
        {
            _taskBacklog.Update(memberID, taskId);
        }

        //this method will return all the task in that same sprint
        public List<TaskBacklog> GetAllTask(int id)
        {
            return _taskBacklog.GetAllTaskDetail(id);
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _taskBacklog.UpdateConnectionId(connectionid, memberid);
        }
    }
}
