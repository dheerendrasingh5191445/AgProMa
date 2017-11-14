using System;
using System.Collections.Generic;
using System.Linq;
using MyNeo4j.model;

namespace MyNeo4j.Repository
{
    public interface IEfficiencyRepository //interface
    {
        List<TaskBacklog> GetEfficiencyForUser(int userId);
        void Update(int id, ChecklistBacklog checklist);
        void CheckAllChecklistStatus(int checklistId);
        void TaskCompleted(int taskId);
        void CheckAllTaskStatus(int taskId);
        void SprintCompleted(int sprintId);
        void CheckAllSprintStatus(int sprintId);
        void ReleasePlanCompleted(int releasePlanId);
    }
    
    public class EfficiencyRepository: IEfficiencyRepository
    {
        private Neo4jDbContext _context;
        public EfficiencyRepository(Neo4jDbContext context)
        {
            _context = context;
        }

        //get efficiency for user 
        public List<TaskBacklog> GetEfficiencyForUser(int userId)
        {
            return _context.Taaskbl.Where(m => m.PersonId == userId && m.Status == TaskBacklogStatus.Completed).ToList();
            
        }

        //update the status and End time of checklist.
        public void Update(int id, ChecklistBacklog checklist) //update checklist
        {
            ChecklistBacklog sign = _context.Checklistbl.FirstOrDefault(p => p.ChecklistId == id);
            sign.ChecklistName = checklist.ChecklistName;
            sign.Status = checklist.Status;
            sign.EndDate = DateTime.Now;
            _context.SaveChanges();
        }

        //check status of all task
        public void CheckAllChecklistStatus(int checklistId)
        {
            try
            {
                int count = 0;
                //get the checklist from Checklist Id.
                var checklist= _context.Checklistbl.FirstOrDefault(m => m.ChecklistId == checklistId);
                //get all checklists from the taskId.
                List<ChecklistBacklog> backlogs = _context.Checklistbl.Where(m=>m.TaskId==checklist.TaskId).ToList();
                //checking status of all the checklist backlogs.
                foreach (var item in backlogs)
                {
                    if (item.Status == true)
                    {
                        count++;
                    }
                }
                if (count == backlogs.Count)
                {
                    //if all tasks are completed
                    TaskCompleted(checklist.TaskId);
                }
                else
                {
                    //change status to Inprogress in case of undoing checklist
                    TaskBacklog taskblog = _context.Taaskbl.FirstOrDefault(m => m.TaskId == checklist.TaskId);
                    taskblog.Status = TaskBacklogStatus.Inprogress;
                    _context.SaveChanges();
                    CheckAllTaskStatus(taskblog.TaskId);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        //update the taskbl and change status to completed.
        public void TaskCompleted(int taskId)
        {
            //get the task from the taskId and make changes in it(status and date).
            var task = _context.Taaskbl.FirstOrDefault(m => m.TaskId == taskId);
            task.Status = TaskBacklogStatus.Completed;
            task.ActualEndDate = DateTime.Now;
            _context.SaveChanges();

            //invoke for checking status of all the checklist.
            CheckAllTaskStatus(taskId);
        }

        //check status of all the tasks.
        public void CheckAllTaskStatus(int taskId)
        {
            try
            {
                int count = 0;
                //get a task from taskid.
                var task = _context.Taaskbl.FirstOrDefault(m => m.TaskId == taskId);
                //get all tasks assigned to a sprint id.
                List<TaskBacklog> tasks= _context.Taaskbl.Where(m => m.SprintId == task.SprintId).ToList();
                //check status for all the tasks.
                foreach (var item in tasks)
                {
                    if(item.Status==TaskBacklogStatus.Completed)
                    {
                        count++;
                    }
                }
                if(count==tasks.Count)
                {
                    //invoke sprint for changing status and date.
                    SprintCompleted(task.SprintId);
                }
                else
                {
                    //change status to Inprogress in case of undoing task
                    SprintBacklog sprint = _context.Sprintbl.FirstOrDefault(m => m.SprintId == task.SprintId);
                    sprint.Status = SprintStatus.Inprogress;
                    _context.SaveChanges();
                    CheckAllSprintStatus(sprint.SprintId);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        //update sprint status and date of a sprint.
        public void SprintCompleted(int sprintId)
        {
            //get a sprint and update changes(status and date).
            var sprint = _context.Sprintbl.FirstOrDefault(m => m.SprintId == sprintId);
            sprint.Status = SprintStatus.Completed;
            sprint.ActualEndDate = DateTime.Now;
            _context.SaveChanges();

            //check all sprint status assign to a release plan id.
            CheckAllSprintStatus(sprintId);
        }

        //check status of all the sprints.
        public void CheckAllSprintStatus(int sprintId)
        {
            try
            {
                int count = 0;
                //get  a sprint from sprint id.
                var sprint = _context.Sprintbl.FirstOrDefault(m => m.SprintId == sprintId);
                //get all sprints assign to a release plan.
                List<SprintBacklog> sprints = _context.Sprintbl.Where(m => m.ReleasePlanId == sprint.ReleasePlanId).ToList();
                //check status for all the sprints.
                foreach (var item in sprints)
                {
                    if (item.Status == SprintStatus.Completed)
                    {
                        count++;
                    }
                }
                if (count == sprints.Count)
                {
                    //invoke release plan with release plan id.
                    ReleasePlanCompleted(sprint.ReleasePlanId);
                }
                else
                {
                    ReleasePlanMaster release = _context.Releasepl.FirstOrDefault(m => m.ReleasePlanId == sprint.ReleasePlanId);
                    release.Status = ReleasePlanStatus.Inprogress;
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        //update releae plan status and release date.
        public void ReleasePlanCompleted(int releasePlanId)
        {
            //get a release plan specific to release plan id.
            var releasePlan = _context.Releasepl.FirstOrDefault(m => m.ReleasePlanId == releasePlanId);
            releasePlan.Status = ReleasePlanStatus.Release;
            releasePlan.ActualReleaseDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}