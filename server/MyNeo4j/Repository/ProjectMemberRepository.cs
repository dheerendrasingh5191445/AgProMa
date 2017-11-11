using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    //interface of project member class
    public interface IProjectMemberRepository
    {
        List<ProjectMember> getMemberDetails(int id);
        void Add_MemberDetails(ProjectMember member);
    }
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        //object of dbcontext class
        public Neo4jDbContext _context;
        //constructor
        public ProjectMemberRepository(Neo4jDbContext context)
        {
            _context = context;
        }
        //this method gets the details of particular project member
        public List<ProjectMember> getMemberDetails(int memberid)
        {
            return _context.Projectmember.Where(p =>p.MemberId == memberid).ToList();
        }
        //this method adds the details of particular member corresponding to projectid
        public void Add_MemberDetails(ProjectMember member)
        {
            _context.Projectmember.Add(member);
            _context.SaveChanges();
        }
    }
}
