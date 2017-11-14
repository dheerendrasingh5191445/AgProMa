using MyNeo4j.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IProjectMemberService
    {
        List<ProjectMember> getMemberDetails(int id);
        int Add_MemberDetails(ProjectMember member);
       
    }
    public class ProjectMemberService : IProjectMemberService
    {
        private IProjectMemberRepository _repo;
        public ProjectMemberService(IProjectMemberRepository repo)
        {
            _repo = repo;
        }

        public List<ProjectMember> getMemberDetails(int memberid)
        {
            
            return _repo.getMemberDetails(memberid).ToList();
        }

        public int Add_MemberDetails(ProjectMember details)
        {
            List<ProjectMember> projectmem = getMemberDetails(details.MemberId);
            foreach (var member in projectmem)
            {
                if ( member.MemberId == details.MemberId && member.ProjectId == details.ProjectId)
                {
                    return 0;
                }                    
            }
            _repo.Add_MemberDetails(details);
            return 1;



        }

    }
}
