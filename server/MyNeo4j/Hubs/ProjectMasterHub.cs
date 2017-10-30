using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Hubs
{
    public class ProjectMasterHub:Hub
    {
        IProjectMasterService _service;
        public ProjectMasterHub(IProjectMasterService service)
        {
            _service = service;
        }
        
        public Task AddNewProject(ProjectMaster promas,string roomName)
        {
           
            _service.AddProject(promas);
            Groups.AddAsync(Context.ConnectionId, roomName);//implement in for each
            Clients.Group(roomName).InvokeAsync("addproject","data that needs to send");
            return null;
        }
    }
}
