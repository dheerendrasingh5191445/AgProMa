using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeo4j.model;
using MyNeo4j.Repository;

namespace MyNeo4j.Service
{
    public interface ICheckListService
    {
        TaskBacklog GetTaskDetail(int Id);
        List<ChecklistBacklog> Get();
        List<ChecklistBacklog> Get(int id);
        void Add_Checklist(ChecklistBacklog addChecklist);
        void Delete(int id);

    }
    public class ChecklistService : ICheckListService
    {
        private ICheckListRepository _context; //repository instance
        public ChecklistService(ICheckListRepository con)
        {
            _context = con;
        }

        public void Add_Checklist(ChecklistBacklog addChecklist)//for adding checklist
        {
            _context.Add_Checklist(addChecklist);
        }

        public void Delete(int id) //deleting checklist
        {
            _context.Delete(id);
        }

        public List<ChecklistBacklog> Get() //getting list
        {
            return _context.Get().ToList();
        }

        public List<ChecklistBacklog> Get(int id)
        {
            return _context.Get(id);
        }

        public TaskBacklog GetTaskDetail(int Id)
        {
            return _context.GetTaskDetail(Id);
        }

    }
}
