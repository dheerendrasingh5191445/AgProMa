using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IProjectMemberService
    {
        ProjectMember getMemberDetails(int id);
        void Add_MemberDetails(ProjectMember member);
       

    }
    public class ProjectMemberService : IProjectMemberService
    {
        public IProjectMemberRepository _repo;
        public ProjectMemberService(IProjectMemberRepository repo)
        {
            _repo = repo;
        }

        public ProjectMember getMemberDetails(int id)
        {
            
            return _repo.getMemberDetails(id);
        }
        public void Add_MemberDetails(ProjectMember details)
        {
            _repo.Add_MemberDetails(details);
        }

    }
}
