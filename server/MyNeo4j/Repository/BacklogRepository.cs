using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{

    public interface IBacklogRepository
    {
        List<ProductBacklog> GetAll(int  id);
        void Add(ProductBacklog bklog);
        void Update(int id, ProductBacklog bklog);
        void Delete(int id);
    }
    public class BacklogRepository : IBacklogRepository
    {

        private Neo4jDbContext _context;
        public BacklogRepository(Neo4jDbContext context)
        {
            _context = context;

        }

        public void Add(ProductBacklog bklog)
        {
            _context.Productbl.Add(bklog);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {


            //selecting the BuildingStructure from the BuildingStructures Table by BuildingCode passed by the client
            ProductBacklog backlogToBeRemoved = _context.Productbl.FirstOrDefault(m => m.StoryId == id);

            //Removing the BuildingStructure object
            _context.Productbl.Remove(backlogToBeRemoved);

            //persisting the changes made to the database
            _context.SaveChanges();
        }


        public List<ProductBacklog> GetAll(int  id)
        {
            return _context.Productbl.Include(f => f.ProjectMaster).Where(r => r.ProjectId == id).ToList() ;
        }

        public void Update(int id, ProductBacklog bklog)
        {
            ProductBacklog data = _context.Productbl.FirstOrDefault(m => m.StoryId == id);

            data.StoryName = bklog.StoryName;

            data.Comments = bklog.Comments;
            _context.SaveChanges();
        }
    }

}



