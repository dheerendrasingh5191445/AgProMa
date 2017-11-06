using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Hubs
{

    public class EpicHub:Hub
    {
        private IEpicServices _service;
        //constructor of epic service
        public EpicHub(IEpicServices service)
        {
            _service = service;
        }
        //this is to set the connection Id for user Id
        public void SetConnectId(int userid)
        {
            _service.SetConnectId(userid,Context.ConnectionId);
        }
        //this method is to get the epic according to project Id 
        public Task Get(int id)
        {
            CreateGroup(id);
            List<EpicMaster> data = _service.GetAll(id);
            return Clients.Group("mygroup").InvokeAsync("getBacklog", data);
        }

        public Task Post(EpicMaster backlog)
        {
            _service.Add(backlog);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenAdded");
        }

        public void CreateGroup(int projectId)
        {
            List<string> users = _service.getGroup(projectId);
            foreach(var user in users)
            {
                Groups.AddAsync(user, "mygroup");
            }
        }

        public Task put(int id,EpicMaster value)
        {
            _service.Update(id,value);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenUpdated");
        }

        public Task Delete(int id)
        {
            _service.Delete(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenDeleted");
        }
    }
}
