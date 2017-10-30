using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeo4j.model;
using Microsoft.EntityFrameworkCore;

namespace MyNeo4j.Repository
{
    public interface ICheckListRepository //declarations
    {
        List<ChecklistBacklog> Get();
        ChecklistBacklog Get(int id);
        void Add_User(ChecklistBacklog adduser);
        void Update(int id, ChecklistBacklog user);
        void Delete(int id);
    }
    public class ChecklistRepository : ICheckListRepository 
    {
        private Neo4jDbContext _context;
        public ChecklistRepository(Neo4jDbContext con) 
        {
            _context = con;

        }
        public void Add_User(ChecklistBacklog adduser) //adding checklist
        {
            _context.Checklistbl.Add(adduser);
            _context.SaveChanges();
        }

        public void Delete(int id) //delete checklist
        {
            ChecklistBacklog f = _context.Checklistbl.FirstOrDefault(p => p.ChecklistId == id);
            _context.Checklistbl.Remove(f);
            _context.SaveChanges();
        }

        public List<ChecklistBacklog> Get() //getting checklist
        {
            return _context.Checklistbl.Include(m=>m.TaskBacklog).ToList();
        }

        public ChecklistBacklog Get(int id) //getting checklist according to task
        {
            ChecklistBacklog s = _context.Checklistbl.FirstOrDefault(p => p.ChecklistId == id);
            return s;
        }

        public void Update(int id, ChecklistBacklog user) //update checklist
        {
            ChecklistBacklog sign = _context.Checklistbl.FirstOrDefault(p => p.ChecklistId == id);
            sign.ChecklistName = user.ChecklistName;
            sign.Status = user.Status;
            _context.SaveChanges();
        }
    }
}
