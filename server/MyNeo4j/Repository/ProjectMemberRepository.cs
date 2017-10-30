using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IProjectMemberRepository
    {
        ProjectMember getMemberDetails(int id);
        void Add_MemberDetails(ProjectMember member);


    }
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        public Neo4jDbContext _context;
        public ProjectMemberRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        public ProjectMember getMemberDetails(int id)
        {
            return _context.Projectmember.FirstOrDefault(p => p.id == id);
        }
        public void Add_MemberDetails(ProjectMember member)
        {
            _context.Projectmember.Add(member);
            _context.SaveChanges();
        }

    }
}
