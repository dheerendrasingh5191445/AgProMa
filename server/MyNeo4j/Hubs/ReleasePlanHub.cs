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
        public void GetReleasePlans(int projectid)
        {
            var data = _service.GetAllReleasePlan(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getreleaseplans", data);
        }
        public Task AddRelease(ReleasePlanMaster release)
        {
            CreateGroup(release.ProjectId);
            _service.AddRelease(release);
            return Clients.Group("releaseGroup").InvokeAsync("postrelease", release);
        }


        public void GetAllSprints(int projectid)
        {
            var data = _service.GetAllSprints(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getsprints", data);
        }


    }
}