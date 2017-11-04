using MyNeo4j.model;
using MyNeo4j.Repository;
using System.Collections.Generic;
namespace MyNeo4j.Service
{
    public interface IReleasePlanService    //Interface
    {
        List<ReleasePlanMaster> GetAllReleasePlan(int id);
        void AddRelease(ReleasePlanMaster releasePlan);
        List<SprintBacklog> GetAllSprints(int id);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
        void UpdateReleaseInSprint(SprintBacklog sprintbl,int releaseId);
    }
    public class ReleasePlanService : IReleasePlanService
    {
        private IReleasePlanRepo _releasePlanRepo;
        private ISprintRepository _sprintRepository;
        public ReleasePlanService(IReleasePlanRepo releasePlanRepo, ISprintRepository sprintRepository) //Constructor
        {
            _releasePlanRepo = releasePlanRepo;
            _sprintRepository = sprintRepository;
        }
        //Method for adding a new release
        public void AddRelease(ReleasePlanMaster releasePlan)
        {
            _releasePlanRepo.AddRelease(releasePlan);
        }
        //Method for getting a list of all release
        public List<ReleasePlanMaster> GetAllReleasePlan(int id)
        {
            List<ReleasePlanMaster> release = new List<ReleasePlanMaster>();
            List<ReleasePlanMaster> allRelease = _releasePlanRepo.GetAllRelease();
            foreach (ReleasePlanMaster rem in allRelease)
            {
                if (rem.ProjectId == id)
                {
                    release.Add(rem);
                }
            }
            return release;
        }
        //Method for getting the list of all sprints
        public List<SprintBacklog> GetAllSprints(int id)
        {
            return _sprintRepository.GetAll(id);
        }
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _releasePlanRepo.UpdateConnectionId(connectionid, memberid);
        }
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            return _releasePlanRepo.CreateGroup(projectid);
        }

        public void UpdateReleaseInSprint(SprintBacklog sprintbl, int releaseId)
        {
            _releasePlanRepo.UpdateReleaseInSprint(sprintbl.SprintId, releaseId);
        }
    }
}