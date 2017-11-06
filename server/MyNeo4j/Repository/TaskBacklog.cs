using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{

    public interface ITaskRepository
    {
        List<SignalRMaster> JoinGroup(int projectId);
        void SetConnectionId(string connectionId,int memberId);
        List<TaskBacklog> GetAll(int sprintId);
        void Add(TaskBacklog bklog);
        void Update(int id, TaskBacklog bklog);
        SprintBacklog GetProjectId(int sprintId);
    }
    public class TaskRepository : ITaskRepository
    {

        private Neo4jDbContext _context;
        public TaskRepository(Neo4jDbContext context)
        {
            _context = context;

        }

        public void Add(TaskBacklog bklog)
        {
            _context.Taaskbl.Add(bklog);
            _context.SaveChanges();
        }

        public List<TaskBacklog> GetAll(int sprintId)
        {
            return _context.Taaskbl.Where(m=>m.SprintId==sprintId).ToList();
        }

        public void Update(int id, TaskBacklog bklog)
        {
            TaskBacklog data = _context.Taaskbl.FirstOrDefault(m => m.TaskId == id);

            data.TaskName = bklog.TaskName;

            //  data. = bklog.Comments;
            _context.SaveChanges();
        }

        //updates the connection fields for the member
        public void SetConnectionId(string connectionId,int memberId)
        {
            SignalRMaster signalMaster = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberId);
            signalMaster.ConnectionId = connectionId;
            signalMaster.HubCode = HubCode.task;
            _context.SaveChanges();
        }

        //get the online member list and returned it
        public List<SignalRMaster> JoinGroup(int projectId)
        {
            List<ProjectMember> projectMembers = _context.Projectmember.Where(m => m.ProjectId == projectId).ToList();
            var memberIds = projectMembers.Select(m => m.MemberId);
            List<SignalRMaster> onlineMembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.task).ToList();
            return onlineMembers.Where(n => memberIds.Contains(n.MemberId)).ToList();
        }

        public SprintBacklog GetProjectId(int sprintId)
        {
            return _context.Sprintbl.FirstOrDefault(p => p.SprintId == sprintId);
        }
    }

}