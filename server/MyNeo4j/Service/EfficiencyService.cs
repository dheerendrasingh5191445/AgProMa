using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IEfficiencyService
    {
        float GetEfficiencyForUser(int userId);
        void Update(int id, ChecklistBacklog checklist);
    }
    public class EfficiencyService : IEfficiencyService
    {
        public IEfficiencyRepository _repository;
        public EfficiencyService(IEfficiencyRepository repository)
        {
            _repository = repository;
        }

        //get efficiency for a user.
        public float GetEfficiencyForUser(int userId)
        {
            //get all tasks assigned to a user.
            List<TaskBacklog> tasks=_repository.GetEfficiencyForUser(userId).ToList();
            float expectedTime = 0, actualTime = 0;
            //get expected time and actual time for all the tasks assigned to single user only.
            foreach (var task in tasks)
            {
                expectedTime += task.EndDate.Subtract(task.StartDate).Days;
                actualTime += task.ActualEndDate.Subtract(task.StartDate).Days;
            }
            return (expectedTime / actualTime) * 100;
        }
        

        public void Update(int id, ChecklistBacklog checklist)
        {
            _repository.Update(id, checklist);
            //chek status of all the checklist assigned to a task
            _repository.CheckAllChecklistStatus(id);
        }
    }
}
