using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface ISocialLoginService
    {
        void AddSocialUser(SocialSignupMaster socialmaster);
    }
    public class SocialLoginService:ISocialLoginService
    {
        //object of dbcontext class
        private ISocialLoginRepository _context;
        //constructor
        public SocialLoginService(ISocialLoginRepository con)
        {
            _context = con;
        }

        public void AddSocialUser(SocialSignupMaster socialmaster)
        {
            _context.AddSocialUser(socialmaster);
        }
    }
}
