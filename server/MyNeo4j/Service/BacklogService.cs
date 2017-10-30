using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IBacklogServices
    {
        List<ProductBacklog> GetAll(int  id);
        List<ProductBacklog> GetUnassignedStory(int projectId);
        void Add(ProductBacklog backlog);
        void Update(int id, ProductBacklog res);
        void Delete(int id);
    }

    public class BacklogService : IBacklogServices
    {
      private  IBacklogRepository _repository;
        public BacklogService( IBacklogRepository repository)
        {
            _repository = repository;
        }

        public void Add(ProductBacklog backlog)
        {
            _repository.Add(backlog);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<ProductBacklog> GetUnassignedStory(int projectId)
        {
            return _repository.GetUnassignedStory(projectId);
        }

        public List<ProductBacklog> GetAll(int  id)
        {
            return _repository.GetAll(id).ToList();
        }

        public void Update(int id, ProductBacklog res)
        {
            _repository.Update(id, res);
        }
    }
}
