using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MyNeo4j.Repository
{
    //Interface
    public interface IReleasePlanRepo
    {
        List<SprintBacklog> GetAllSprints(int projectId);
        List<ReleasePlanMaster> GetAllRelease(int projectId);
        void AddRelease(ReleasePlanMaster releasePlan);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
        void UpdateReleaseInSprint(int sprintId,int releaseId);
    }
    public class ReleasePlanRepo : IReleasePlanRepo
    {
        Neo4jDbContext _context;
        //Constructor
        public ReleasePlanRepo(Neo4jDbContext context)
        {
            _context = context;
        }
        //Method for adding a new release
        public void AddRelease(ReleasePlanMaster releasePlan)
        {
            _context.Releasepl.Add(releasePlan);
            _context.SaveChanges();
        }
        //Method for getting the list of all release to a particular project id
        public List<ReleasePlanMaster> GetAllRelease(int projectId)
        {
            return _context.Releasepl.Where(m=>m.ProjectId==projectId).ToList();
        }
        //Method for getting the list of all sprints to a particular project id
        public List<SprintBacklog> GetAllSprints(int projectId)
        {
            return _context.Sprintbl.Where(m=>m.ProjectId==projectId).ToList();
        }
        //Method for updating a connection id through a member id
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.releaseplan;
            _context.SaveChanges();
        }
        //Method for creating a group to a particular project id
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            List<ProjectMember> members = _context.Projectmember.Where(m => m.ProjectId == projectid).ToList();
            List<SignalRMaster> onlinemembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.sprint).ToList();
            var data = members.Select(m => m.MemberId).ToList();
            var users = onlinemembers.Where(om => data.Contains(om.MemberId)).ToList();
            return users;
        }
        //Method for updating a release to a particular sprint id
        public void UpdateReleaseInSprint(int sprintId, int releaseId)
        {
            SprintBacklog sprbl = _context.Sprintbl.FirstOrDefault(p => p.SprintId == sprintId);
            sprbl.ReleasePlanId = releaseId;
            _context.SaveChanges();
        }
    }
}
