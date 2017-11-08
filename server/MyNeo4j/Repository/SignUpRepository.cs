using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgProMa.Repository
{
    //interface of sign-up
    public interface ISignUpRepository
    {
        void UpdatePassword(int id, Master master);
        Master Get(string emailid);
        void Add_User(Master adduser );
        void Update(string emailid, Master user);
        List<Master> GetAllDetails();
        Master GetById(int id);
    }
    public class SignUpRepository : ISignUpRepository
    {
        //object of dbcontext class
        private Neo4jDbContext _context;
        //constructor
        public SignUpRepository(Neo4jDbContext con)
        {
            _context = con;
        }
        //this method shows the list of all registered user details 
        public List<Master> GetAllDetails()
        {
            return _context.Pmaster.ToList();
        }
        //this method adds a particular user
        public void Add_User(Master adduser)
        {
            _context.Pmaster.Add(adduser);
            _context.SaveChanges();
        }
        //this method get the details of a particular user    
        public Master Get(string emailid)
        {
            Master s = _context.Pmaster.FirstOrDefault(p => p.Email == emailid);
            return s;
        }
        //this method updates the details of particular user
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

        public Master GetById(int id)
        {
            return _context.Pmaster.FirstOrDefault(m => m.Id == id);
        }

        public void UpdatePassword(int id, Master master)
        {
            var user = _context.Pmaster.FirstOrDefault(m => m.Id == id);
            user.Password = master.Password;
            _context.SaveChanges();
        }
    }
}
