using Microsoft.AspNetCore.Mvc;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Controllers
{
    [Produces("application/json")]
    [Route("api/ReleasePlan")]
    public class ReleasePlanController : Controller
    {
        IReleasePlanService _releasePlanService;
        public ReleasePlanController(IReleasePlanService releasePlanService)
        {
            _releasePlanService = releasePlanService;
        }

        // GET api/Release
        [HttpGet("GetRelease/{id}")]
        public List<ReleasePlanMaster> GetRelease(int id)
        {
            List<ReleasePlanMaster> data = _releasePlanService.GetAllReleasePlan(id);
            return data;
        }
        //GET api/Sprint
        [HttpGet("GetSprint/{id}")]
        public List<SprintBacklog> GetSprint(int id)
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
