﻿using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Hubs
{
    public class TaskBacklogHub:Hub
    {
        private ITaskBacklogService task;

        public TaskBacklogHub(ITaskBacklogService tservice)
        {
            task = tservice;
        }

        public void SetConnectionId(int Memberid)
        {
            //call method to add memberinfo into db with connectionid and memberid
            task.UpdateConnectionId(Context.ConnectionId, Memberid);
        }

        //this method will return all the task in that same sprint
        public Task GetAllTaskDetail(int id)
        {
            List<TaskBacklog> tasks = task.GetAllTask(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getAllTaskDetail", tasks);
        }

        public Task GetTeamList(int id)
        {
            List<TeamMaster> teams = task.GetByTeamId(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getTeamList", teams);
        }

        public Task GetTeamMember(int teamId)
        {
            List<AvailTeamMember> availablelist = task.getTeamMember(teamId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getTeamMember", availablelist);
        }

        public Task AssignTask(int id,AvailableMember member)
        {
            task.UpdateTask(member.MemberId, id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenAssigned", "success");
        }
    }
}