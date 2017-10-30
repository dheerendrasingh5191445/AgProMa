using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IMasterService
    {
        Master getUserDetailsService(int id);
        void updateDetails(int id, Master details);

    }
    public class MasterService : IMasterService
    {
        public IMasterRepository _repo;
        public MasterService(IMasterRepository repo)
        {
            _repo = repo;
        }

        public Master getUserDetailsService(int id)
        {
            Master master =  _repo.getDetailsOfUser(id);
            return master;
        }
      public  void updateDetails(int id, Master details)
        {
            _repo.updateDetails(id, details);
        }

    }
}
