using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface ITaskServices
    {
        List<SignalRMaster> JoinGroup(int projectId);
        void SetConnectionId(string connectionId,int memberId);
        List<TaskBacklog> GetAll(int sprintId);
        void Add(TaskBacklog backlog);
        void Update(int id, TaskBacklog res);
        int GetProjectId(int sprintId);
    }

    public class TaskService : ITaskServices
    {
        private ITaskRepository _repository;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public void Add(TaskBacklog backlog)
        {
            _repository.Add(backlog);
        }

        public List<TaskBacklog> GetAll(int sprintId)
        {
            return _repository.GetAll(sprintId).ToList();
        }

        public void Update(int id, TaskBacklog res)
        {
            _repository.Update(id, res);
        }

        public void SetConnectionId(string connectionId,int memberId)
        {
            _repository.SetConnectionId(connectionId,memberId);
        }

        public List<SignalRMaster> JoinGroup(int projectId)
        {
            return _repository.JoinGroup(projectId);
        }

        public int GetProjectId(int sprintId)
        {
            SprintBacklog sp = _repository.GetProjectId(sprintId);
            return sp.ProjectId;
        }
    }
}