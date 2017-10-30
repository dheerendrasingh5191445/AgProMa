using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgProMa.Services;
using MyNeo4j.model;

namespace AgProMa.Controllers
{
    [Produces("application/json")]
    public class LoginController : Controller
    {
        private ISignUpService _context;
        public LoginController(ISignUpService context)
        {

            _context = context;
        }
       // GET api/values
       [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            //exception handling
            try
            {
                List<Master> list = _context.GetAllDetails();
                if (list.Count != 0)
                {
                    return Ok(list);
                }
                else
                {
                    return StatusCode(404);
                }

            }
            catch
            {

                return StatusCode(500);
            }
        }

        // GET api/values/5
        [Route("api/[controller]/{emailid}")]
        //this method get the details of a particular user
        public IActionResult Get(string emailid)
        {
            return Ok(_context.Get(emailid));
        }

        // POST api/values
        
        [HttpPost]
        [Route("api/[controller]")]
        //this method adds a user details
        public void PostUserDetails([FromBody]Master user)
        {
            _context.Add_User(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Route("api/[controller]")]
        //this method updates the user details
        public IActionResult Put(string emailid, [FromBody]Master user)
        {
            //exceptional handling
            try
            {
                _context.Update(emailid, user);
                return Ok("success");
            }
            catch
            {
                return Ok("internal server error");

            }
        }
    }
}
