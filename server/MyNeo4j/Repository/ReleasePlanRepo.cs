using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MyNeo4j.Repository
{
    public interface IReleasePlanRepo   //Interface
    {
        List<SprintBacklog> GetAllSprints();
        List<ReleasePlanMaster> GetAllRelease();
        void AddRelease(ReleasePlanMaster releasePlan);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
        void UpdateReleaseInSprint(int sprintId,int releaseId);
    }
    public class ReleasePlanRepo : IReleasePlanRepo
    {
        Neo4jDbContext _context;
        public ReleasePlanRepo(Neo4jDbContext context)  //Constructor
        {
            _context = context;
        }
        //Method for adding a new release
        public void AddRelease(ReleasePlanMaster releasePlan)
        {
            _context.Releasepl.Add(releasePlan);
            _context.SaveChanges();
        }
        //Method for getting the list of all release
        public List<ReleasePlanMaster> GetAllRelease()
        {
            return _context.Releasepl.ToList();
        }
        //Method for getting the list of all sprints
        public List<SprintBacklog> GetAllSprints()
        {
            return _context.Sprintbl.ToList();
        }
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.sprint;
            signalr.Online = true;
            _context.SaveChanges();
        }
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            List<ProjectMember> members = _context.Projectmember.Where(m => m.ProjectId == projectid).ToList();
            List<SignalRMaster> onlinemembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.sprint).ToList();
            var data = members.Select(m => m.MemberId).ToList();
            var users = onlinemembers.Where(om => data.Contains(om.MemberId)).ToList();
            return users;
        }

        public void UpdateReleaseInSprint(int sprintId, int releaseId)
        {
            SprintBacklog sprbl = _context.Sprintbl.FirstOrDefault(p => p.SprintId == sprintId);
            sprbl.ReleasePlanId = releaseId;
            _context.SaveChanges();
        }
    }
}
