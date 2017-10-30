using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface ISprintService
    {
        List<SprintBacklog> GetAll(int projectId);
        SprintBacklog Get(int sprintId);
        void Add(SprintBacklog sprint);
        void Update(int sprintId,SprintBacklog sprint);
        void Delete(int sprintId);
    }
    public class SprintService : ISprintService
    {
        public ISprintRepository _repository;
        public SprintService(ISprintRepository repository)
        {
            _repository = repository;
        }
        public void Add(SprintBacklog sprint)
        {
            _repository.Add(sprint);
        }

        public void Delete(int sprintId)
        {
            _repository.Delete(sprintId);
        }

        public SprintBacklog Get(int sprintId)
        {
            return _repository.Get(sprintId);
        }

        public List<SprintBacklog> GetAll(int projectId)
        {
            return _repository.GetAll(projectId);
        }
        
        public void Update(int sprintId, SprintBacklog sprint)
        {
            _repository.Update(sprintId, sprint);
        }
    }
}
