using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface IBurndownRepository
    {
        List<TaskBacklog> GetTasks(int userId);
        List<SprintBacklog> GetSprints(int projectId);
    }
    public class BurndownRepository : IBurndownRepository
    {
        public Neo4jDbContext _context;
        public BurndownRepository(Neo4jDbContext context)
        {
                _context=context;
        }

        public List<SprintBacklog> GetSprints(int projectId)
        {
            return _context.Sprintbl.Where(m => m.ProjectId == projectId && m.Status==SprintStatus.Completed).ToList();
        }

        //get all tasks assigned to a user
        public List<TaskBacklog> GetTasks(int userId)
        {
            return _context.Taaskbl.Where(m => m.PersonId == userId && m.Status==TaskBacklogStatus.Completed).ToList();
        }
    }
}
