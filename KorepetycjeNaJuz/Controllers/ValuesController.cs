using AutoMapper;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ValuesController(IMapper mapper)
        {
            this._mapper = mapper;
            this._logger = LogManager.GetLogger("apiLogger");
        }

        // GET api/values
        [HttpGet]
        public ActionResult<UserDTO> Get()
        {
            _logger.Info("Simple example of logger");
            // AutoMapper simple example
            User user = new User()
            {
                Id = 1,
                Description = "Example User",
                Email = "user@example.com",
                FirstName = "Janusz",
                LastName = "Admin",
                IsCoach = true,
                Telephone = "656-233-222"
            };
            UserDTO userDTO = this._mapper.Map<UserDTO>(user);

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
