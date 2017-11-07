using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface ITaskBacklogReposiory
    {
        List<TaskBacklog> getByTaskId();
        List<TeamMaster> getTeamName();
        List<SprintBacklog> AllSprint();
        List<TeamMember> AllTeamMember();
        SprintBacklog GetProjectId(int Id);
        Master Master(int id);
        void UpdateConnectionId(string connectionid, int memberid);
        List<TaskBacklog> GetAllTaskDetail(int id);
        void Update(int memberId, int TaskId);
        List<ProjectMember> AllMember(int projectId);
    }
    public class TaskBacklogRepository : ITaskBacklogReposiory
    {
        private Neo4jDbContext _context;
        public TaskBacklogRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        public Master Master(int id)
        {
            return _context.Pmaster.FirstOrDefault(p => p.Id == id);
        }

        public List<SprintBacklog> AllSprint()
        {
            return _context.Sprintbl.ToList();
        }

        public List<TeamMember> AllTeamMember()
        {
            return _context.Teammemeber.ToList();
        }

        public List<TaskBacklog> getByTaskId()
        {
            return _context.Taaskbl.ToList();

        }
        public List<TeamMaster> getTeamName()
        {
            return _context.Teammaster.ToList();
        }

        public void Update(int memberId, int TaskId)
        {
            TaskBacklog task = _context.Taaskbl.FirstOrDefault(p => p.TaskId == TaskId);
            task.PersonId = memberId;
            _context.SaveChanges();
        }

        //this method will return all the task in that same sprint from database
        public List<TaskBacklog> GetAllTaskDetail(int SprintId)
        {
            return _context.Taaskbl.Where(p => p.SprintId == SprintId).ToList();
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.taskbl;
            _context.SaveChanges();
        }

        public List<ProjectMember> AllMember(int projectId)
        {
            return _context.Projectmember.Where(p => p.ProjectId == projectId).ToList();
        }

        public SprintBacklog GetProjectId(int Id)
        {
            return _context.Sprintbl.FirstOrDefault(p => p.SprintId == Id);
        }
    }
}