using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyNeo4j.model;
using MyNeo4j.Service;

namespace MyNeo4j.Controllers
{
    // [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TaskController : Controller
    {
        private ITaskServices _service;

        public TaskController(ITaskServices service)
        {
            _service = service;

        }
        // GET: api/backlog
        [HttpGet]
        public IEnumerable<TaskBacklog> Get()
        {

            return null;
        }
        // GET api/values/5
       [HttpGet("{id}")]
        public TaskBacklog Get(int id)
        {
            return null;
        }
        [HttpPost]
        public void Post([FromBody]TaskBacklog backlog)
        {
            _service.Add(backlog);

        }

        [HttpPut("{id}")]
        public void put(int id, [FromBody]TaskBacklog value)
        {
          
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           
        }
    }
}