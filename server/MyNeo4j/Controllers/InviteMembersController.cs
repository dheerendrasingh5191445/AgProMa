using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForgetPassword.service;
using MyNeo4j.Service;
using MyNeo4j.Viewmodel;

namespace ForgetPassword.Controllers
{
    [Produces("application/json")]
    public class InviteMembersController : Controller
    {

        private IinviteMembersService _service;

        public InviteMembersController(IinviteMembersService service)
        {
            _service = service;
        }


        // PUT: api/ForgetPassword/5
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult post([FromBody]InvitePeople people)
        {
            try
            {
                _service.EmailForInvitation(people); //mail for invite people
                return new NoContentResult();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.StackTrace);//handling bad request
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500); //internal server error
            }
        }

        //this method is to fetch the data
       [HttpGet]
       [Route("api/InviteMembers/{id}")]
       public List<AvailableMember> GetMemberName(int id)
        {
            return _service.GetMemberName(id);
        }

    }
}
