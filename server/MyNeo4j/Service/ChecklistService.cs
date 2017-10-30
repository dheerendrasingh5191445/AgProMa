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
        List<ChecklistBacklog> Get();
        ChecklistBacklog Get(int id);
        void Add_User(ChecklistBacklog addItem);
        void Update(int id, ChecklistBacklog addItem);
        void Delete(int id);

    }
    public class ChecklistService : ICheckListService
    {
        private ICheckListRepository _context; //repository instance
        public ChecklistService(ICheckListRepository con)
        {
            _context = con;
        }
        public void Add_User(ChecklistBacklog addItem) //for adding checklist
        {
            _context.Add_User(addItem);
        }

        public void Delete(int id) //deleting checklist
        {
            _context.Delete(id);
        }

        public List<ChecklistBacklog> Get() //getting list
        {
            return _context.Get().ToList();
        }

        public ChecklistBacklog Get(int id) //getting checklist according to task
        {
            return _context.Get(id);
        }

        public void Update(int id, ChecklistBacklog addItem) //updating checklist
        {
            _context.Update(id, addItem);
        }
    }
}
