﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNeo4j.Service;
using MyNeo4j.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNeo4j.Controllers
{
    [Produces("application/json")]
   
    public class ChecklistController : Controller 
    {
        private ICheckListService _context; // creating service instance
        public ChecklistController(ICheckListService context)
        {

            _context = context;
        }
        // GET: api/values
        [HttpGet]
        [Route("api/[controller]")]
        public List<ChecklistBacklog> Get()
        {
            List<ChecklistBacklog> f = _context.Get(); //getting checklist
            return f;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ChecklistBacklog Get(int id)
        {
            ChecklistBacklog a = _context.Get(id); // getting checklist according to task
            return a;
        }

        // POST api/values
        [HttpPost]
        [Route("api/[controller]")]
        public void Post([FromBody]ChecklistBacklog value) //adding checklist
        {
            _context.Add_User(value);
        }

        // PUT api/values/5
        [HttpPut]
        [Route("api/[controller]/{id}")]
        public void Put(int id, [FromBody]ChecklistBacklog value) //update checklist
        {
            _context.Update(id, value);
           
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/[controller]/{id}")] 
        public void Delete(int id) //delete checklist
        {
            _context.Delete(id);
        }
    }
}