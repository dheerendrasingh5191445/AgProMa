using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IBurndownService
    {
        List<UserBurnDown> GetTasks(int userId);
        List<SprintBacklog> GetSprints(int projectId);
    }

    public class BurndownService : IBurndownService
    {
        public IBurndownRepository _repository;
        public BurndownService(IBurndownRepository repository)
        {
            _repository = repository;
        }

        public List<SprintBacklog> GetSprints(int projectId)
        {
            return _repository.GetSprints(projectId);
        }

        public List<UserBurnDown>  GetTasks(int userId)
        {
            List<UserBurnDown> dropdown = new List<UserBurnDown>();
            List<TaskBacklog> tasks= _repository.GetTasks(userId);
            tasks.ForEach(m => {
                UserBurnDown userbd = new UserBurnDown();
                userbd.TaskId = m.TaskId;
                userbd.TaskName = m.TaskName;
                userbd.ExpectedDate = m.EndDate.Subtract(m.StartDate).TotalHours;
                userbd.ActualDate = m.ActualEndDate.Subtract(m.StartDate).TotalHours;
                dropdown.Add(userbd);
            });
            return dropdown;
        }
    }
}
