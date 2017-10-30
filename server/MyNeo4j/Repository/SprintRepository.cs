using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface ISprintRepository
    {
        List<SprintBacklog> GetAll(int projectId);
        SprintBacklog Get(int sprintId);
        void Add(SprintBacklog sprint);
        void Update(int sprintId, ProductBacklog sprint);
        void Delete(int sprintId);
    }

    public class SprintRepository : ISprintRepository
    {
        public Neo4jDbContext _context;
        public SprintRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        //add a new sprint.
        public void Add(SprintBacklog sprint)
        {
            _context.Sprintbl.Add(sprint);
            _context.SaveChanges();
        }

        //delete particular sprint.
        public void Delete(int sprintId)
        {
            SprintBacklog sprint=_context.Sprintbl.FirstOrDefault(m => m.SprintId == sprintId);
            _context.Sprintbl.Remove(sprint);
            _context.SaveChanges();
        }

        //get the sprint backlog.
        public SprintBacklog Get(int sprintId)
        {
            return _context.Sprintbl.FirstOrDefault(m => m.SprintId == sprintId);
        }

        //get all sprint backlogs.
        public List<SprintBacklog> GetAll(int projectId)
        {
            return _context.Sprintbl.Where(m => m.ProjectId == projectId).ToList();
        }
        
        //updates sprint details of the specific sprint.
        public void Update(int sprintId, ProductBacklog sprint)
        {
            ProductBacklog sprints = _context.Productbl.FirstOrDefault(m => m.StoryId == sprint.StoryId);
            sprints.InSprintNo = sprintId;
            _context.SaveChanges();
        }
    }
}
