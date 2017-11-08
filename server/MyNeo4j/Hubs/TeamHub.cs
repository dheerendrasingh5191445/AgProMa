using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Hubs
{
    public class TeamHub:Hub
    {
        public ITeamService _service;
        public TeamHub(ITeamService service)
        {
            _service = service;
        }

        public void SetConnectionId(int Memberid)
        {
            //call method to add memberinfo into db with connectionid and memberid
            _service.UpdateConnectionId(Context.ConnectionId, Memberid);
        }

        public Task AddTeam(TeamMaster team)
        {
            _service.addTeam(team);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenAdded", "success");
        }

        public Task UpdateteamMember(TeamMember member,int projectId)
        {
             _service.addMembers(member);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenUpdated","success");
        }

        public Task Delete(int id,int projectId)
        {
            _service.deleteMember(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenDeleted", "success");
        }

        public Task GetTeams(int id)
        {
             List<TeamMaster> teams =_service.getTeam(id);
             return Clients.Client(Context.ConnectionId).InvokeAsync("getTeams", teams);
        }

        public Task GetAvailableMember(int projectId)
        {
             List<AvailTeamMember> alist =_service.getAvailableMember(projectId);
             return Clients.Client(Context.ConnectionId).InvokeAsync("getAvailableMember", alist);
        }
    }
}

