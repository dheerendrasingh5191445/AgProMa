using Microsoft.EntityFrameworkCore;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface IInviteRepository
    {
        List<ProjectMember> GetMemberName();
        Master AllData(int id);
    }
    public class InviteRepository : IInviteRepository
    {
        public Neo4jDbContext _context;
        public InviteRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        public Master AllData(int id)
        {
            return _context.Pmaster.FirstOrDefault(p => p.Id == id);
        }

        public List<ProjectMember> GetMemberName()
        {
            return _context.Projectmember.ToList();
        }
    }
}