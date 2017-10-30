using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface IReleasePlanRepo   //Interface
    {
        List<SprintBacklog> GetAllSprints();
        List<ReleasePlanMaster>GetAllRelease();
        void AddRelease(ReleasePlanMaster releasePlan);
    }
    public class ReleasePlanRepo : IReleasePlanRepo
    {
        Neo4jDbContext _context;
        public ReleasePlanRepo(Neo4jDbContext context)  //Constructor
        {
            _context = context;
        }

        //Method for adding a new release
        public void AddRelease(ReleasePlanMaster releasePlan)
        {
            _context.Releasepl.Add(releasePlan);
            _context.SaveChanges();
        }

        //Method for getting the list of all release
        public List<ReleasePlanMaster> GetAllRelease()
        {
            return _context.Releasepl.ToList();
        }

        //Method for getting the list of all sprints
        public List<SprintBacklog> GetAllSprints()
        {
            return _context.Sprintbl.ToList();
        }
    }
}
