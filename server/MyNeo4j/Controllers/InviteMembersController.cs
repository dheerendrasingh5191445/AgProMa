using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForgetPassword.service;
using MyNeo4j.Service;
using MyNeo4j.Viewmodel;
using Microsoft.AspNetCore.Authorization;
using MyNeo4j.model;
using AgProMa.Services;

namespace ForgetPassword.Controllers
{

    [Produces("application/json")]
    public class InviteMembersController : Controller
    {

        private IinviteMembersService _service;
        private ISignUpService _context;
        public InviteMembersController(IinviteMembersService service, ISignUpService context)
        {
            _service = service;
            _context = context;
        }

        //get all details of all users
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
       public List<InviteExistingMember> GetMemberName(int id)
        {
            return _service.GetMemberName(id);
        }

    }
}
