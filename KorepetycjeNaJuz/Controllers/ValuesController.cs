using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ValuesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<UserDTO> Get()
        {
            // AutoMapper simple example
            var user = new User() {
                Id = 1,
                Description = "Example User",
                Email = "user@example.com",
                FirstName = "Janusz",
                LastName = "Admin",
                IsCoach = true,
                Telephone = "656-233-222"
            };
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
