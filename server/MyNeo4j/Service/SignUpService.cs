using AgProMa.Repository;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgProMa.Services
{
    //interface for sign-up
    public interface ISignUpService
    {
        Master Get(string emailid);
        void Add_User(Master favourite);
        void Update(string emailid, Master favourite);
        List<Master> GetAllDetails();
    }
    public class SignUpService : ISignUpService
    {
        //object of repository class
        private ISignUpRepository _context;
        //constructor
        public SignUpService (ISignUpRepository con)
        {
            _context = con;
        }
        //this method shows the list of all registered users
        public List<Master> GetAllDetails()
        {
            return _context.GetAllDetails();
        }
        //this method adds the user
        public void Add_User(Master user)
        {
            _context.Add_User(user);
        }
        //this method get the details of particular user
        public Master Get(string emailid)
        {
            return _context.Get(emailid);
        }
        //this method updates the detail of particular user
        public void Update(string emailid, Master user)
        {
            _context.Update(emailid, user);
        }
    }
}