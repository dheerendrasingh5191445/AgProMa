using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System.Threading.Tasks;
namespace MyNeo4j.Hubs
{
    public class ReleasePlanHub : Hub
    {
        private IReleasePlanService _service;
        public ReleasePlanHub(IReleasePlanService service)
        {
            _service = service;
        }

        public void SetConnectionId(int Memberid)
        {
            //call method to add memberinfo into db with connectionid and memberid
            _service.UpdateConnectionId(Context.ConnectionId, Memberid);
        }

        public void CreateGroup(int projectid)
        {
            var users = _service.CreateGroup(projectid);
            foreach (var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "releaseGroup");
            }
        }


        public Task GetReleasePlans(int projectId)
        {
            CreateGroup(projectId);
            var data = _service.GetAllReleasePlan(projectId);
            return Clients.Group("releaseGroup").InvokeAsync("getreleaseplans", data);
        }
        public Task AddRelease(ReleasePlanMaster release)
        {
            CreateGroup(release.ProjectId);
            _service.AddRelease(release);
            return Clients.Group("releaseGroup").InvokeAsync("postrelease", release);
        }

        public Task GetAllSprints(int projectId)
        {
            CreateGroup(projectId);
            var data = _service.GetAllSprints(projectId);
            return Clients.Group("releaseGroup").InvokeAsync("getsprints", data);
        }

        public void UpdateReleaseInSprint(SprintBacklog sprintbl,int releaseId)
        {
            CreateGroup(sprintbl.ProjectId);
            _service.UpdateReleaseInSprint(sprintbl,releaseId);
            GetAllSprints(sprintbl.ProjectId);

        }


    }
}