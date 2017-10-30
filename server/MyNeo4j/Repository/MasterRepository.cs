using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{
    public interface IMasterRepository
    {
        Master getDetailsOfUser(int id);
        void updateDetails(int id, Master details);
        List<Master> getAll();
    }
    public class MasterRepository : IMasterRepository
    {
        private Neo4jDbContext _context;
        public MasterRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        public List<Master> getAll()
        {
            return _context.Pmaster.ToList();
        }

        public Master getDetailsOfUser(int userId)
        {
            Master master = _context.Pmaster.FirstOrDefault(p => p.Id == userId);
            return master;
        }
        public void updateDetails(int id,Master details)
        {
            Master m = _context.Pmaster.FirstOrDefault(p => p.Id == id);
            m.Password = details.Password;

            _context.SaveChanges();
        }
    }
}


