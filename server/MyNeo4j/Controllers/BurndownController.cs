using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNeo4j.Service;
using MyNeo4j.model;
using MyNeo4j.Viewmodel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNeo4j.Controllers
{
    //[Route("api/[controller]")]
    public class BurndownController : Controller
    {
        public IBurndownService _service;
        public BurndownController(IBurndownService service)
        {
            _service = service;
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/[controller]/GetTasks/{id}")]
        //get all tasks assigned to a user.
        public List<UserBurnDown> GetTasks(int id)
        {
            return _service.GetTasks(id);
        }
        
        [HttpGet]
        [Route("api/[controller]/GetProjectData/{id}")]
        //get all project details for a project.
        public List<ReleasePlanMaster> GetProjectData(int id)
        {
            return _service.GetProjectData(id);
        }

        [HttpGet]
        [Route("api/[controller]/GetSprintDetails/{id}")]
        //get all project details for a project.
        public List<SprintBacklog> GetSprintDetails(int id)
        {
            return _service.GetSprintDetails(id);
        }
    }
}
