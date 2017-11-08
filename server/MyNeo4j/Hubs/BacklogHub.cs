using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MyNeo4j.Hubs
{
    public class BacklogHub : Hub
    {
        private IBacklogServices _service;
        public BacklogHub(IBacklogServices service)
        {
            _service = service;
        }

        //set and update connection id for the user
        public void setConnectionId(int memberId)
        {
            _service.setConnectionId(Context.ConnectionId,memberId);
        }

        //create a group and member join it each time they visited this Hub
        public void JoinGroup(int projectId)
        {
            var users=_service.JoinGroup(projectId);
            foreach(var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "BacklogGroup");
            }
        }
        
        //get the backlogs specific to projectId
        public void GetBacklog(int projectid)
        {
            var data = _service.GetAll(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getbacklog", data);
        }

        //add the backlog
        public Task PostBacklog(ProductBacklog data)
        {
            JoinGroup(data.ProjectId);
            _service.Add(data);
            return Clients.Group("BacklogGroup").InvokeAsync("postBacklog", data);
        }

        //update the backlog(user story) according to storyId   
        public Task UpdateBacklog(int storyId,ProductBacklog product)
        {
            JoinGroup(product.ProjectId);
            ProductBacklog backlog= _service.Update(storyId, product);
            return Clients.Group("BacklogGroup").InvokeAsync("updateBacklog",backlog);
        }

        // delete a backlog(user story) particular to storyId
        public Task DeleteBacklog(int storyId,int projectId)
        {
            JoinGroup(projectId);
            _service.Delete(storyId);
            return Clients.Group("BacklogGroup").InvokeAsync("deleteBacklog", storyId);
        }
    }
}