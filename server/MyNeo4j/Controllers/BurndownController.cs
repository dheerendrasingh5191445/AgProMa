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
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
