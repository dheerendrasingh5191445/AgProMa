using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System.Threading.Tasks;
namespace MyNeo4j.Hubs
{
    public class ReleasePlanHub : Hub
    {
        private IReleasePlanService _service;
        //constructor
        public ReleasePlanHub(IReleasePlanService service)
        {
            _service = service;
        }

        public void SetConnectionId(int Memberid)
        {
            //call method to add memberinfo into db with connectionid and memberid
            _service.UpdateConnectionId(Context.ConnectionId, Memberid);
        }
        //method for creating a group to particular project id
        public void CreateGroup(int projectid)
        {
            var users = _service.CreateGroup(projectid);
            foreach (var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "releaseGroup");
            }
        }
        //method for getting all release plans to a aprticular project id
        public Task GetReleasePlans(int projectId)
        {
            CreateGroup(projectId);
            var data = _service.GetAllReleasePlan(projectId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getreleaseplans", data);
        }
        //method for adding a new release
        public Task AddRelease(ReleasePlanMaster release)
        {
            CreateGroup(release.ProjectId);
            _service.AddRelease(release);
            return Clients.Group("releaseGroup").InvokeAsync("whenAdded", release);
        }
        //method for getting all sprints to a particular project id
        public Task GetAllSprints(int projectId)
        {
            CreateGroup(projectId);
            var data = _service.GetAllSprints(projectId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getsprints", data);
        }
        //Method for updating a release to a particular a sprint 
        public void UpdateReleaseInSprint(SprintBacklog sprintbl,int releaseId)
        {
            CreateGroup(sprintbl.ProjectId);
            _service.UpdateReleaseInSprint(sprintbl,releaseId);
            GetAllSprints(sprintbl.ProjectId);

        }
    }
}