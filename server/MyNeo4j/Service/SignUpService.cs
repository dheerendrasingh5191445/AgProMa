using AgProMa.Repository;
using MyNeo4j.model;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgProMa.Services
{
    //interface for sign-up
    public interface ISignUpService
    {

        void UpdatePassword(int id, Master master);
        string logOut(int userId);
        Creadential Check(IdPassword cread);
        string Add_User(Master favourite);
        void Update(string emailid, Master favourite);
        List<Master> GetAllDetails();
        int GetId(string email);
        Master GetById(int id);

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
        public string Add_User(Master user)
        {
            Master master = _context.Get(user.Email);
            if (master == null)
            {
                _context.Add_User(user);
                return "success";
            }
            else
                return "exist";
            

        }
        //this method get the details of particular user
        public Creadential Check(IdPassword cread)
        {
            Creadential cre = new Creadential();
            Master master = _context.Get(cread.Email);
            if (master == null)
            {
                cre.Status = "notexist";
                return cre;
            }
            else if (master.Email == cread.Email && master.Password == cread.Password)
            {
                _context.SetLoggedIn(master.Id, true);
                cre.Status = "success";
                cre.UserId = master.Id;
                cre.UserName = master.FirstName;
                cre.Email = master.Email;
                return cre;
            }
            else if (master.Email == cread.Email && master.Password != cread.Password)
            {
                cre.Status = "exist";
                return cre;
            }
            else
            {
                cre.Status = "error";
                return cre;
            } 
           
        }
        //this method updates the detail of particular user
        public void Update(string emailid, Master user)
        {
            _context.Update(emailid, user);
        }

        //this is to get  user id according to email
        public int GetId(string email)
        {
            try
            {
                Master master = _context.Get(email);
                return master.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Master GetById(int id)
        {
            return _context.GetById(id);
        }

        public void UpdatePassword(int id, Master master)
        {
            _context.UpdatePassword(id, master);
        }
        //this is to logout from the system
        public string logOut(int userId)
        {
           bool response = _context.SetLoggedIn(userId, false);
            if (response == true) { return "success"; }
            else return "failed";

        }
    }
}