using System;
using AutoMapper;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly KorepetycjeContext _context;

        public UsersController(KorepetycjeContext context)
        {
            this._context = context;
            this._logger = LogManager.GetLogger("apiLogger");
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            _logger.Info("Entered GetUsers method.");
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            _logger.Info(string.Format("Entered GetUser({0}) method.", id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            _logger.Info(string.Format("Executing PutUser({0}) method...", id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest();

            bool _userExists = UserExists(id);
            User _userTmp;

            if (!_userExists) return NotFound();
            else _userTmp = _context.Users.Find(id);

            _context.Entry(user).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException e)
            {
                UserControllerException uce = new UserControllerException(string.Format("Database update operation was impossible due to the following cause:\n", e.Message));
                _logger.Error(uce.Message);
                throw uce;
            }

            _logger.Info(string.Format("User {0} {1} has been modified.", _userTmp.FirstName, _userTmp.LastName));

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            _logger.Info("Executing PostUser() method...");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.Info(string.Format("User {0} {1} has been added ", user.FirstName, user.LastName));

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            _logger.Info(string.Format("Executing DeleteUser({0}) method...", id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.Info(string.Format("User with id={0} has been deleted.", id));

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public class UserControllerException : ApplicationException
        {
            public UserControllerException(string message) : base(message) { }
        }
    }
}