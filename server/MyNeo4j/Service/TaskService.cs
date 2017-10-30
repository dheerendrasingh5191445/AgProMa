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
        List<TaskBacklog> GetAll();
        TaskBacklog Get(int id);
        void Add(TaskBacklog backlog);
        void Update(int id, TaskBacklog res);
        void Delete(int id);
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

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<TaskBacklog> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public TaskBacklog Get(int id)
        {
            return _repository.Get(id);
        }

        public void Update(int id, TaskBacklog res)
        {
            _repository.Update(id, res);
        }
    }
}