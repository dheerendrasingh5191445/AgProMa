using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface IBurndownRepository
    {
        List<ReleasePlanMaster> GetProjectData(int projectId);
        List<TaskBacklog> GetTasks(int userId);
    }
    public class BurndownRepository : IBurndownRepository
    {
        public Neo4jDbContext _context;
        public BurndownRepository(Neo4jDbContext context)
        {
                _context=context;
        }

        //get project details for a project.
        public List<ReleasePlanMaster> GetProjectData(int projectId)
        {
            return _context.Releasepl.Include(m=>m.Sprints).ThenInclude(m=>m.Tasks).Where(m => m.ProjectId == projectId).ToList();
        }

        //get all tasks assigned to a user
        public List<TaskBacklog> GetTasks(int userId)
        {
            return _context.Taaskbl.Include(n=>n.SprintBacklogs.ProjectMaster).Where(m => m.PersonId == userId && m.Status==TaskBacklogStatus.Completed).ToList();
        }
    }
}
