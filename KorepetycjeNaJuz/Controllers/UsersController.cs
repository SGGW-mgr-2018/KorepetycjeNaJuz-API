using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.DTO.User;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KorepetycjeContext _context;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
    
        public UsersController(IUserService userService, KorepetycjeContext context)
        {
            this._logger = LogManager.GetLogger("apiLogger");
            this._userService = userService;
            this._context = context;
        }

        /// <summary>
        /// Pozwala pobrać wszystkich użytkowników
        /// </summary>
        /// <returns>Zwraca JSON zawierający dane wszystkich użytkowników z bazy danych</returns>
        /// <response code="200">Lista użytkowników</response>
        /// <response code="401">Wymagana autoryzacja</response>
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), 200), ProducesResponseType(401)]
        [HttpGet, Route("GetAll"), Authorize("Bearer")]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            _logger.Info("Entered GetUsers method.");
            return await _userService.GetAllUsersAsync();
        }

        /// <summary>
        /// Pozwala pobrać konkretnego użytkownika na podstawie ID
        /// </summary>
        /// <param name="id">Numer ID wybranego użytkownika</param>
        /// <returns>Dane użytkownika o podanym ID</returns>
        /// <response code="200">Dane użytkownika o podanym ID</response>
        /// <response code="400">Przekazano niepoprawne zapytanie bez numeru ID</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
        [ProducesResponseType(typeof(UserDTO), 200), ProducesResponseType(400)]
        [ProducesResponseType(401), ProducesResponseType(404)]
        [HttpGet, Route("Get/{id}"), Authorize("Bearer")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            _logger.Info(string.Format("Entered GetUser({0}) method.", id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetUserAsync(id);

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
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
        [ProducesResponseType(204), ProducesResponseType(400)]
        [ProducesResponseType(401), ProducesResponseType(404)]
        [HttpPut, Route("Update/{id}"), Authorize("Bearer")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            _logger.Info(string.Format("Executing PutUser({0}) method...", id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest();

            bool isUserExists = await _userService.IsUserExistsAsync(id);
            if (!isUserExists)
                return NotFound();

            PropertyValues _oldVals = _context.Entry(user).GetDatabaseValues();
            string _oldEmail = _oldVals.GetValue<string>("Email");
            string _oldPhoneNumber = _oldVals.GetValue<string>("PhoneNumber");

            if (user.Email != _oldEmail)
                user.EmailConfirmed = false;
            else
                user.EmailConfirmed = true;
            if (user.PhoneNumber != _oldPhoneNumber)
                user.PhoneNumberConfirmed = false;
            else
                user.PhoneNumberConfirmed = true;

            _context.Entry(user).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException e)
            {
                UserControllerException uce = new UserControllerException(string.Format("Database update operation was impossible due to the following cause:\n", e.Message));
                _logger.Error(uce.Message);
                return StatusCode(304, uce.Message);
            }

            _logger.Info(string.Format("User with id={0} has been modified.", id));

            return NoContent();
        }        

        /// <summary>
        /// Rejestracja użytkownika w bazie danych
        /// </summary>
        /// <param name="userCreateDTO">JSON zawierający wszystkie wymagane dane użytkownika</param>
        /// <returns>JSON ze wszystkimi danymi nowego użytkownika</returns>
        /// <response code="201">Poprawnie utworzono użytkownika</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
        [ProducesResponseType(201), ProducesResponseType(400)]
        [HttpPost, Route("Create"), AllowAnonymous]
        public async Task<IActionResult> PostUser([FromBody] UserCreateDTO userCreateDTO)
        {
            _logger.Info("Executing PostUser() method...");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Custom Validation
            var isEmailExists = await _userService.IsEmailExistsAsync(userCreateDTO.Email);
            if (isEmailExists)
            {
                ModelState.AddModelError("Email", "Podany adres email istnieje w bazie.");
                return BadRequest(ModelState);
            }

            bool result = await _userService.CreateUserAsync(userCreateDTO);
            if (!result)
            {
                _logger.Error("Error during user registration");
                return BadRequest();
            }

            _logger.Info(string.Format("User {0} {1} has been added ", userCreateDTO.FirstName, userCreateDTO.LastName));

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Usunięcie użytkownika o podanym ID z bazy danych
        /// </summary>
        /// <param name="id">Numer ID użytkownika wskazanego do usunięcia</param>
        /// <returns>JSON zawierający dane usuniętego użytkownika</returns>
        /// <response code="200">Poprawnie usunięto użytkownika</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
        [HttpDelete, Route("Delete/{id}"), Authorize("Bearer")]
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
        
        /// <summary>
        /// Akceptacja regulaminów przez użytkownika.
        /// </summary>
        /// <param name="policy">Udzielone zgody</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500)]
        [HttpPut, Route("UpdateUserPolicy")]
        //[Authorize("Bearer")]
        public async Task<IActionResult> UpdateUserAcceptancePolicy([FromBody]PolicyDTO policy)
        {
            try
            {
                var currentUserId = 1;// User.GetUserId().Value;
                if (policy.AcceptPrivacy) await _userService.AcceptPrivacy(currentUserId);
                if (policy.AcceptRODO) await _userService.AcceptRODO(currentUserId);
                if (policy.AcceptCookies) await _userService.AcceptCookies(currentUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during Message creation");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        /// <summary>
        /// Zresetowanie zgód użytkowników po zmianie regulaminów.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500)]
        [HttpPut]
        //[Authorize("Admin")]
        public async Task<IActionResult> UpgradePolicy()
        {
            try
            {
                await _userService.ClearPolicyAcceptanceAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during Message creation");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        public class UserControllerException : ApplicationException
        {
            public UserControllerException(string message) : base(message) { }
        }
    }
}