using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{

    public interface IEpicServices
    {
        List<EpicMaster> GetAll(int id);
        void Add(EpicMaster backlog);
        void Update(int id, EpicMaster res);
        void Delete(int id);
    }

    public class EpicService:IEpicServices
    {
        
   private IEpicRepository _repository;
    public EpicService(IEpicRepository repository)
    {
        _repository = repository;
    }

    public void Add(EpicMaster backlog)
    {
        _repository.Add(backlog);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public List<EpicMaster> GetAll(int id)
    {
        return _repository.GetAll(id).ToList();
    }

    public void Update(int id, EpicMaster res)
    {
        _repository.Update(id, res);
    }
}
}