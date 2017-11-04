﻿using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Hubs
{
    public class SprintBacklogHub : Hub
    {
        private ISprintService _service;
        private IBacklogServices _backlogService;

        public SprintBacklogHub(ISprintService service,IBacklogServices backlogService)
        {
            _service = service;
            _backlogService = backlogService;
        }

        //call method to add memberinfo into db with connectionid and memberid
        public void SetConnectionId(int memberId)
        {
            _service.UpdateConnectionId(Context.ConnectionId, memberId);
        }
        public void CreateGroup(int projectid)
        {
            var users = _service.CreateGroup(projectid);
            foreach (var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "SprintGroup");
            }

        }
        public void GetSprints(int projectid)
        {
            List<SprintBacklog> data = _service.GetAll(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getSprints",data);
        }

        public Task AddSprint(SprintBacklog sprint)
        {
            CreateGroup(sprint.ProjectId);
            _service.Add(sprint);
            return Clients.Group("SprintGroup").InvokeAsync("postSprints", sprint);
        }

        public void UpdateStoryInSprint(ProductBacklog sprint, int sprintid,int projectid)
        {
            CreateGroup(projectid);
            _service.Update(sprintid,sprint);
            GetAllBacklogs(projectid);
        }

        public Task GetAllBacklogs(int projectId)
        {
            CreateGroup(projectId);
            List<ProductBacklog> backlogs=_backlogService.GetAll(projectId);
            return Clients.Group("SprintGroup").InvokeAsync("getBacklogs", backlogs);
        }

    }
}
