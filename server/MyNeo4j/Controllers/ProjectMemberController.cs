using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgProMa.Services;
using MyNeo4j.model;
using MyNeo4j.Service;

namespace AgProMa.Controllers
{
    [Produces("application/json")]
    public class ProjectMemberController : Controller
    {
        private IProjectMemberService _context;
        public ProjectMemberController(IProjectMemberService context)
        {

            _context = context;
        }
        // GET api/values
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            return Ok("oidsjicd");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public void PostMemberDetails([FromBody]ProjectMember user)
        {
            _context.Add_MemberDetails(user);
        }

       
    }
}
