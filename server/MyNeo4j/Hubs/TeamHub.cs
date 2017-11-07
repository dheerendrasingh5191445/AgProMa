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

        //this method will add team to a project
        public Task AddTeam(TeamMaster team)
        {
            _service.AddTeam(team);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenAdded", "success");
        }

        //this method will add members to a team 
        public Task UpdateteamMember(TeamMember member,int projectId)
        {
             _service.AddMembers(member);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenUpdated","success");
        }

        //this method will delete member from a team
        public Task Delete(int id,int projectId)
        {
            _service.DeleteMember(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenDeleted", "success");
        }

        //this method will return teams of a project
        public Task GetTeams(int id)
        {
             List<TeamMaster> teams =_service.GetTeam(id);
             return Clients.Client(Context.ConnectionId).InvokeAsync("getTeams", teams);
        }

        //this method will return available members in a project
        public Task GetAvailableMember(int projectId)
        {
             List<AvailTeamMember> alist =_service.GetAvailableMember(projectId);
             return Clients.Client(Context.ConnectionId).InvokeAsync("getAvailableMember", alist);
        }
    }
}

