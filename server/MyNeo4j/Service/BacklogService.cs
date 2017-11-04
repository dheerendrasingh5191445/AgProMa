﻿using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IBacklogServices
    {
        void setConnectionId(string connecitonId, int memberId);
        List<SignalRMaster> JoinGroup(int projectId);
        List<ProductBacklog> GetAll(int  id);
        void Add(ProductBacklog backlog);
        ProductBacklog Update(int id, ProductBacklog res);
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
        
        public List<ProductBacklog> GetAll(int  id)
        {
            return _repository.GetAll(id).ToList();
        }

        public ProductBacklog Update(int id, ProductBacklog res)
        {
            return _repository.Update(id, res);
        }

        //update the connection for the user
        public void setConnectionId(string connectionId, int memberId)
        {
            _repository.setConnectionId(connectionId,memberId);
        }

        //get the online members.
        public List<SignalRMaster> JoinGroup(int projectId)
        {
            return _repository.JoinGroup(projectId);
        }
    }
}
