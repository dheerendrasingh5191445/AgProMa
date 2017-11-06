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
        void SetConnectId(int userId,string conId);
        List<string> getGroup(int projectId);
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

    public List<string> getGroup(int projectId)
    {
            List<string> signal = new List<string>();
            List<ProjectMember> promem = _repository.GetMemberIdList(projectId);
            foreach(ProjectMember pro in promem)
            {
                SignalRMaster entry = _repository.GetConnectIdByMemId(pro.MemberId);
                if (entry.HubCode == 0 && entry.Online == true)
                { signal.Add(entry.ConnectionId); }
            }
            return signal;
    }

        public void SetConnectId(int userId,string conId)
    {
        _repository.SetConnectId(userId, conId);        
    }

        public void Update(int id, EpicMaster res)
    {
        _repository.Update(id, res);
    }
}
}