﻿using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{

    public interface IBacklogRepository
    {
        void setConnectionId(string connecitonId, int memberId);
        List<SignalRMaster> JoinGroup(int projectId);
        List<ProductBacklog> GetAll(int  id);
        void Add(ProductBacklog bklog);
        ProductBacklog Update(int id, ProductBacklog bklog);
        void Delete(int id);
    }
    public class BacklogRepository : IBacklogRepository
    {

        private Neo4jDbContext _context;
        public BacklogRepository(Neo4jDbContext context)
        {
            _context = context;
        }
 
        //for adding new item into the product backlog table
        public void Add(ProductBacklog bklog)
        {
            //Adding  the ProductBacklog object
            _context.Productbl.Add(bklog);
            //persisting the changes made to the database
            _context.SaveChanges();
        }
 
        //for deleting any item based on the storyid
        public void Delete(int id)
        {
            //selecting  all the user story from the ProductBacklog Table by storyId passed by the client
            ProductBacklog backlogToBeRemoved = _context.Productbl.FirstOrDefault(m => m.StoryId == id);

            //Removing the ProductBacklog object
            _context.Productbl.Remove(backlogToBeRemoved);

            //persisting the changes made to the database
            _context.SaveChanges();
        }

        //for getting all user story based on project id
        public List<ProductBacklog> GetAll(int  id)
        {
            //stories having projectid equals to id   will be fetched 
            return _context.Productbl.Include(f => f.ProjectMaster).Where(r => r.ProjectId == id).ToList() ;
        }
 
        //for updating a particular user story based on story id.
        public ProductBacklog Update(int storyId, ProductBacklog bklog)
        {
            ProductBacklog data = _context.Productbl.FirstOrDefault(m => m.StoryId == storyId);
 
            //data will be updated
            data.StoryName = bklog.StoryName;
            data.Priority = bklog.Priority;
            data.Comments = bklog.Comments;
            _context.SaveChanges();
            return data;
        }
 
        //update the connection for a member.
        public void setConnectionId(string connectionId,int memberId)
        {
            SignalRMaster signalMaster = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberId);
            signalMaster.ConnectionId = connectionId;
            signalMaster.Online = true;
            signalMaster.HubCode = HubCode.backlog;
            //persisting the changes made to the database
            _context.SaveChanges();
        }
 
        //online members and on same component will get join the group.
        public List<SignalRMaster> JoinGroup(int projectId)
        {
            List<ProjectMember> projectMembers = _context.Projectmember.Where(m => m.ProjectId == projectId).ToList();
            var memberIds = projectMembers.Select(m => m.MemberId);
            List<SignalRMaster> onlineMembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.backlog).ToList();
            return onlineMembers.Where(n => memberIds.Contains(n.MemberId)).ToList();
        }s
    }

}



