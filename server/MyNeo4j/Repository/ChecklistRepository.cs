using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeo4j.model;
using Microsoft.EntityFrameworkCore;

namespace MyNeo4j.Repository
{
    public interface ICheckListRepository //interface
    {
        TaskBacklog GetTaskDetail(int Id);
        List<ChecklistBacklog> Get();
        List<ChecklistBacklog> Get(int id);
        void Add_Checklist(ChecklistBacklog addchecklist);
        void Update(int id, ChecklistBacklog checklist);
        void Delete(int id);
    }
    public class ChecklistRepository : ICheckListRepository 
    {
        private Neo4jDbContext _context;
        public ChecklistRepository(Neo4jDbContext con) 
        {
            _context = con;

        }
        public void Add_Checklist(ChecklistBacklog addchecklist) //adding checklist
        {
            addchecklist.StartDate = DateTime.Now;
            _context.Checklistbl.Add(addchecklist);
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

        public List<ChecklistBacklog> Get(int id) //getting checklist according to task
        {
            return _context.Checklistbl.Where(Id => Id.TaskId == id).ToList();
        }

        public TaskBacklog GetTaskDetail(int Id)
        {
            return _context.Taaskbl.FirstOrDefault(p => p.TaskId == Id);
        }

        public void Update(int id, ChecklistBacklog checklist) //update checklist
        {
            ChecklistBacklog sign = _context.Checklistbl.FirstOrDefault(p => p.ChecklistId == id);
            sign.ChecklistName = checklist.ChecklistName;
            sign.Status = checklist.Status;
            sign.EndDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
