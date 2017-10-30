using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgProMa.Repository
{
    public interface ISignUpRepository
    {
        Master Get(string emailid);
        void Add_User(Master adduser );
        void Update(string emailid, Master user);
        List<Master> GetAllDetails();
    }
    public class SignUpRepository : ISignUpRepository
    {
        private Neo4jDbContext _context;
        public SignUpRepository(Neo4jDbContext con)
        {
            _context = con;
        }
        public List<Master> GetAllDetails()
        {
            return _context.Pmaster.ToList();
        }
        public void Add_User(Master adduser)
        {
            _context.Pmaster.Add(adduser);
            _context.SaveChanges();
        }

        public Master Get(string emailid)
        {
            Master s = _context.Pmaster.FirstOrDefault(p => p.Email == emailid);
            return s;
        }

        public void Update(string emailid, Master s)
        {
            Master sign = _context.Pmaster.FirstOrDefault(p => p.Email == emailid);
            sign.Password = s.Password;
            
            sign.Department = s.Department;
            sign.Organization = s.Organization;
            sign.FirstName = s.FirstName;
            sign.LastName = s.LastName;
            _context.SaveChanges();

        }
    }
}
