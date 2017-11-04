using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{

    public interface ITaskRepository
    {
        List<TaskBacklog> GetAll();
        TaskBacklog Get(int id);
        void Add(TaskBacklog bklog);
        void Update(int id, TaskBacklog bklog);
        void Delete(int id);
    }
    public class TaskRepository : ITaskRepository
    {

        private Neo4jDbContext _context;
        public TaskRepository(Neo4jDbContext context)
        {
            _context = context;

        }

        public void Add(TaskBacklog bklog)
        {
            _context.Taaskbl.Add(bklog);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {


            //selecting the BuildingStructure from the BuildingStructures Table by BuildingCode passed by the client
            TaskBacklog backlogToBeRemoved = _context.Taaskbl.FirstOrDefault(m => m.TaskId == id);

            //Removing the BuildingStructure object
            _context.Taaskbl.Remove(backlogToBeRemoved);

            //persisting the changes made to the database
            _context.SaveChanges();
        }






        public List<TaskBacklog> GetAll()
        {
            return _context.Taaskbl.ToList();
        }
        public TaskBacklog Get(int id)
        {
            TaskBacklog s = _context.Taaskbl.FirstOrDefault(p => p.TaskId == id);
            return s;
        }

        public void Update(int id, TaskBacklog bklog)
        {
            TaskBacklog data = _context.Taaskbl.FirstOrDefault(m => m.TaskId == id);

            data.TaskName = bklog.TaskName;

          //  data. = bklog.Comments;
            _context.SaveChanges();
        }


    }

}