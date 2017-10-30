using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeo4j.Service;
using MyNeo4j.model;

namespace MyNeo4j.Controllers
{
   // [Produces("application/json")]
    [Route("api/[Controller]")]
    public class BacklogController:Controller {
        private IBacklogServices _service;

        public BacklogController(IBacklogServices service)
        {
            _service = service;

        }
        // GET: api/backlog
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<ProductBacklog> Get( int  id)
        {
            List<ProductBacklog> data = _service.GetAll(id);
                return data;
           
        }

        [HttpGet]
        [Route("GetUnassignedStory/{id}")]
        public List<ProductBacklog> GetUnassignedStory(int id)
        {
            return _service.GetUnassignedStory(id);
        }

        // GET: api/Master/5
        //    [HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Master
        [HttpPost]
         public void Post([FromBody]ProductBacklog backlog)
    {
            _service.Add(backlog);

    }
      
        [HttpPut("{id}")]
        public void put(int id, [FromBody]ProductBacklog value)
        {
            _service.Update(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}