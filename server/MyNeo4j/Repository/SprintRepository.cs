using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface ISprintRepository
    {
        List<SprintBacklog> GetAll(int projectId);
        SprintBacklog Get(int sprintId);
        void Add(SprintBacklog sprint);
        void Update(int sprintId, ProductBacklog sprint);
        void Delete(int sprintId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
    }

    public class SprintRepository : ISprintRepository
    {
        public Neo4jDbContext _context;
        public SprintRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        //add a new sprint.
        public void Add(SprintBacklog sprint)
        {
            _context.Sprintbl.Add(sprint);
            _context.SaveChanges();
        }

        //delete particular sprint.
        public void Delete(int sprintId)
        {
            SprintBacklog sprint = _context.Sprintbl.FirstOrDefault(m => m.SprintId == sprintId);
            _context.Sprintbl.Remove(sprint);
            _context.SaveChanges();
        }

        //get the sprint backlog.
        public SprintBacklog Get(int sprintId)
        {
            return _context.Sprintbl.FirstOrDefault(m => m.SprintId == sprintId);
        }

        //get all sprint backlogs.
        public List<SprintBacklog> GetAll(int projectId)
        {
            return _context.Sprintbl.Where(m => m.ProjectId == projectId).ToList();
        }

        //updates sprint details of the specific sprint.
        public void Update(int sprintId, ProductBacklog sprint)
        {
            ProductBacklog sprints = _context.Productbl.FirstOrDefault(m => m.StoryId == sprint.StoryId);
            sprints.InSprintNo = sprintId;
            _context.SaveChanges();
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
            List<ProjectMember> members = _context.Projectmember.Where(p => p.ProjectId == projectid).ToList();

            List<SignalRMaster> onlinemembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.sprint).ToList();
            var data = members.Select(m => m.MemberId).ToList();
            var users = onlinemembers.Where(om => data.Contains(om.MemberId)).ToList();
            return users;
        }









    }
}

