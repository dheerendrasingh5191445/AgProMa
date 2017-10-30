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
            //try
            //{
            //    List<Master> list = _context.GetAllDetails();
            //    if (list.Count != 0)
            //    {
            //        return Ok(list);
            //    }
            //    else
            //    {
            //        return StatusCode(404);
            //    }

            //}
            //catch
            //{

            //    return StatusCode(500);
            //}
        }

        // GET api/values/5
        //[Route("api/[controller]/{emailid}")]
        //public IActionResult Get(string emailid)
        //{
        //    return Ok(_context.Get(emailid));
        //}

        // POST api/values

        [HttpPost]
        [Route("api/[controller]")]
        public void PostMemberDetails([FromBody]ProjectMember user)
        {
            _context.Add_MemberDetails(user);
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //[Route("api/[controller]")]
        //public IActionResult Put(string emailid, [FromBody]Master user)
        //{
        //    try
        //    {
        //        _context.Update(emailid, user);
        //        return Ok("success");
        //    }
        //    catch
        //    {
        //        return Ok("internal server error");

        //    }
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
