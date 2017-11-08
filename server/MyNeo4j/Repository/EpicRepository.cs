using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{

    public interface IEpicRepository
    {
        List<EpicMaster> GetAll(int id);
        void Add(EpicMaster bklog);
        void Update(int id, EpicMaster bklog);
        void Delete(int id);
        void SetConnectId(int userId,string conId);
        List<SignalRMaster> GetAllConnect();
        List<ProjectMember> GetMemberIdList(int id);
        SignalRMaster GetConnectIdByMemId(int memberId);
    }
    public class EpicRepository:IEpicRepository
    { 
  private Neo4jDbContext _context;
    public EpicRepository(Neo4jDbContext context)
    {
        _context = context;

    }

    public void Add(EpicMaster bklog)
    {
        _context.EpicDb.Add(bklog);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {


        //selecting the BuildingStructure from the BuildingStructures Table by BuildingCode passed by the client
        EpicMaster backlogToBeRemoved = _context.EpicDb.FirstOrDefault(m => m.EpicId == id);

        //Removing the BuildingStructure object
        _context.EpicDb.Remove(backlogToBeRemoved);

        //persisting the changes made to the database
        _context.SaveChanges();
    }




    public List<EpicMaster> GetAll(int id)
    {
        return _context.EpicDb.Include(f => f.ProjectMaster).Where(r => r.ProjectId == id).ToList();
    }

    public List<SignalRMaster> GetAllConnect()
    {
            return _context.SignalRDb.ToList();
    }

        public List<ProjectMember> GetMemberIdList(int id)
        {
            List<ProjectMember> memidlist = new List<ProjectMember>();
            memidlist = _context.Projectmember.Where(p => p.ProjectId == id).ToList();
            return memidlist;

        }

        public void SetConnectId(int userId,string conId)
    {
           SignalRMaster sg = _context.SignalRDb.FirstOrDefault(p => p.MemberId == userId);
            sg.ConnectionId = conId;
            sg.HubCode = HubCode.epic;
           _context.SaveChanges();
    }

        public SignalRMaster GetConnectIdByMemId(int memberId)
        {
            return _context.SignalRDb.FirstOrDefault(p => p.MemberId == memberId);
        }

        public void Update(int id, EpicMaster bklog)
    {
        EpicMaster data = _context.EpicDb.FirstOrDefault(m => m.EpicId == id);

        data.Description = bklog.Description;

      
        _context.SaveChanges();
    }
}

}



