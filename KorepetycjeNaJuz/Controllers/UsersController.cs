using System;
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

        /// <summary>
        /// Pozwala pobrać wszystkich użytkowników
        /// </summary>
        /// <returns>Zwraca JSON zawierający dane wszystkich użytkowników z bazy danych</returns>
        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            _logger.Info("Entered GetUsers method.");
            return _context.Users;
        }

        /// <summary>
        /// Pozwala pobrać konkretnego użytkownika na podstawie ID
        /// </summary>
        /// <param name="id">Numer ID wybranego użytkownika</param>
        /// <returns>Dane użytkownika o podanym ID</returns>
        /// <response code="400">Przekazano niepoprawne zapytanie bez numeru ID</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
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

        /// <summary>
        /// Edycja użytkownika o wskazanym numerze ID
        /// </summary>
        /// <param name="id">Numer ID wybranego użytkownika</param>
        /// <param name="user">JSON zawierający wszystkie dane użytkownika</param>
        /// <returns>Kod odpowiedzi HTTP 204, 400 lub 404</returns>
        /// <response code="204">Poprawnie edytowano użytkownika</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
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
            else
            { // Logic
                _userTmp = _context.Users.Find(id);
                if (user.Email != _userTmp.Email)
                    user.EmailConfirmed = false;
                else
                    user.EmailConfirmed = true;
                if (user.PhoneNumber != _userTmp.PhoneNumber)
                    user.PhoneNumberConfirmed = false;
                else
                    user.PhoneNumberConfirmed = true;
            }

            _context.Entry(user).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException e)
            {
                UserControllerException uce = new UserControllerException(string.Format("Database update operation was impossible due to the following cause:\n", e.Message));
                _logger.Error(uce.Message);
                return StatusCode(304, uce.Message);
            }

            _logger.Info(string.Format("User {0} {1} has been modified.", _userTmp.FirstName, _userTmp.LastName));

            return NoContent();
        }

        /// <summary>
        /// Rejestracja użytkownika w bazie danych
        /// </summary>
        /// <param name="user">JSON zawierający wszystkie wymagane dane użytkownika</param>
        /// <returns>JSON ze wszystkimi danymi nowego użytkownika</returns>
        /// <response code="201">Poprawnie utworzono użytkownika</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
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

        /// <summary>
        /// Usunięcie użytkownika o podanym ID z bazy danych
        /// </summary>
        /// <param name="id">Numer ID użytkownika wskazanego do usunięcia</param>
        /// <returns>JSON zawierający dane usuniętego użytkownika</returns>
        /// <response code="200">Poprawnie usunięto użytkownika</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
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