using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeo4j.Service;
using MyNeo4j.model;
using MyNeo4j.Viewmodel;

namespace MyNeo4j.Controllers
{
    [Produces("application/json")]
    [Route("api/TaskBacklog")]
    public class TaskBacklogController : Controller
    {
        private ITaskBacklogService task;

        public TaskBacklogController(ITaskBacklogService tservice)
        {
            task = tservice;
        }

        [HttpGet("GetAllTaskDetail/{id}")]
        public List<TaskBacklog> GetAllTaskDetail(int id)
        {
            return task.getAllTask(id);
        }

        [HttpGet("GetByTeamId/{id}")]
        public List<TeamMaster> GetByTeamId(int id)
        {
            return task.GetByTeamId(id);
        }

        [HttpGet("GetTask/{id}")]
        public List<TaskBacklog> GetTask(int id)
        {
            return task.getByTaskId(id);
        }

        [HttpGet("GetTeamMember/{id}")]
        public List<AvailTeamMember> GetTeamMember(int id)
        {
            return task.getTeamMember(id);
        }

        [HttpGet("GetMemberName/{id}")]
        public string GetMemberName(int id)
        {
            return task.getName(id);
        }

        // PUT: api/TaskBacklog/5
        [HttpPut("{id}")]
        public void Put(int id,[FromBody]AvailableMember member)
        {
            task.UpdateTask(member.MemberId, id);
        }
        
    }
}
