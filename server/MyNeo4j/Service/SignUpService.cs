using AgProMa.Repository;
using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgProMa.Services
{
    public interface ISignUpService
    {
         Master Get(string emailid);
        void Add_User(Master favourite);
        void Update(string emailid, Master favourite);
        List<Master> GetAllDetails();
    }
    public class SignUpService : ISignUpService
    {
        private ISignUpRepository _context;
        public SignUpService (ISignUpRepository con)
        {
            _context = con;
        }
        public List<Master> GetAllDetails()
        {
            return _context.GetAllDetails();
        }
        public void Add_User(Master user)
        {
            _context.Add_User(user);
        }

        public Master Get(string emailid)
        {
            return _context.Get(emailid);
        }

        public void Update(string emailid, Master user)
        {
            _context.Update(emailid, user);
        }
    }
}
