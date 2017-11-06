using MyNeo4j.model;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{ //interface of project creation and manipulation repository
    public interface IProjectMasterRepo
    {
        List<ProjectMember> GetRoleOf();
        List<ProjectMaster> GetAllProjects();
        void AddNewProject(ProjectMaster promas);
        void DeleteProject(int Id);
        void UpdateProject(int Id,ProjectMaster promas);
        void AddProjectMemberL(ProjectMember promem);
    }
    //class used for containing method of this repository
    public class ProjectMasterRepo : IProjectMasterRepo
    {   
        //constructor of this method
        Neo4jDbContext _context;
        public ProjectMasterRepo(Neo4jDbContext context)
        {
            _context = context;
        }
        //method to add new project
        public void AddNewProject(ProjectMaster promas)
        {
            _context.ProjectM.Add(promas);
            _context.SaveChanges();
        }
        //this is to delete the project
        public void DeleteProject(int Id)
        {
            ProjectMaster promas = _context.ProjectM.FirstOrDefault(p => p.ProjectId == Id);
            _context.ProjectM.Remove(promas);
            _context.SaveChanges();
        }

        //method to get all the project in our table
        public List<ProjectMaster> GetAllProjects()
        {
            return _context.ProjectM.ToList();
        }

        //this project is used to update the project
        public void UpdateProject(int Id, ProjectMaster promas)
        {
            ProjectMaster promaster = _context.ProjectM.FirstOrDefault(p => p.ProjectId == Id);
            promaster.ProjectDescription = promas.ProjectDescription;
            promaster.TechnologyUsed = promas.TechnologyUsed;
            promaster.Name = promas.Name;
            _context.SaveChanges();
        }

        //this method is used for Adding leader in project member table
        public void AddProjectMemberL(ProjectMember promem)
        {
            _context.Projectmember.Add(promem);
            _context.SaveChanges();
        }
        //this is to get the role of particular user id
        public List<ProjectMember> GetRoleOf()
        {
            return _context.Projectmember.ToList();
        }
    }
}
