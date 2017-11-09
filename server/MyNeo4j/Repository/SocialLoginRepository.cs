using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Repository
{

    public interface ISocialLoginRepository
    {
        void AddSocialUser(SocialSignupMaster socialmaster);
        SocialSignupMaster GetSocialDetails(string email);
    }

    public class SocialLoginRepository : ISocialLoginRepository
    {
        //object of dbcontext class
        private Neo4jDbContext _context;
        //constructor
        public SocialLoginRepository(Neo4jDbContext con)
        {
            _context = con;
        }

        public void AddSocialUser(SocialSignupMaster socialmaster)
        {
            _context.Add(socialmaster);
        }

        public SocialSignupMaster GetSocialDetails(string email)
        {
            SocialSignupMaster socialdetails = _context.Socialsm.FirstOrDefault(m => m.Email == email);
            return socialdetails;
        }
    }
}
