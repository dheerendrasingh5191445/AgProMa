using Microsoft.AspNetCore.Mvc;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Controllers
{
    [Route("api/[controller]")]
    public class ReleasePlanController : Controller
    {
        IReleasePlanService _releasePlanService;
        public ReleasePlanController(IReleasePlanService releasePlanService)
        {
            _releasePlanService = releasePlanService;
        }

        // GET api/Release
        [HttpGet]
        public IActionResult GetRelease()
        {
            List<ReleasePlanMaster> data = _releasePlanService.GetAllReleasePlan();
            return Ok(data);
        }
        //GET api/Sprint
        [HttpGet("{id}")]
        public IEnumerable<SprintBacklog> GetSprint(int id)
        {
            List<SprintBacklog> data = _releasePlanService.GetAllSprints(id);
            return data;
        }

        // POST api/Release
        [HttpPost]
        public void Post([FromBody]ReleasePlanMaster releasePlan)
        {
            _releasePlanService.AddRelease(releasePlan);
        }
    }
}
