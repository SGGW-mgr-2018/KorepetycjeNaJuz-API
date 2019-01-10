using System;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOAuthService _oAuthService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
    
        public UsersController(
            IUserService userService,
            IOAuthService oAuthService,
            UserManager<User> userManager)
        {
            this._logger = LogManager.GetLogger("apiLogger");
            this._userService = userService;
            this._oAuthService = oAuthService;
            this._userManager = userManager;
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
            if (!ModelState.IsValid || id == 0) // Użytkownik System
                return BadRequest(ModelState);

            var user = await _userService.GetUserAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Edycja użytkownika o wskazanym numerze ID
        /// </summary>
        /// <param name="userEditDTO">JSON zawierający wszystkie dane użytkownika</param>
        /// <returns>Kod odpowiedzi HTTP 204, 400 lub 404</returns>
        /// <response code="200">Poprawnie edytowano użytkownika</response>
        /// <response code="304">Wystąpił błąd podczas edycji danych</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Nie znaleziono użytkownika o podanym numerze ID</response>
        [ProducesResponseType(typeof(UserDTO), 200), ProducesResponseType(304), ProducesResponseType(400)]
        [ProducesResponseType(401), ProducesResponseType(404)]
        [HttpPut, Route("Update"), Authorize("Bearer")]
        public async Task<IActionResult> PutUser([FromBody] UserEditDTO userEditDTO)
        {
            _logger.Info(string.Format("Executing PutUser({0}) method...", userEditDTO.Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isUserExists = await _userService.IsUserExistsAsync(userEditDTO.Id);
            if (!isUserExists)
                return NotFound();

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userEditDTO);
                _logger.Info(string.Format("User with id={0} has been modified.", updatedUser.Id));

                return Ok(updatedUser);
            }
            catch (DbUpdateConcurrencyException e)
            {
                UserControllerException uce = new UserControllerException(string.Format("Database update operation was impossible due to the following cause:\n", e.Message));
                _logger.Error(uce.Message);
                return StatusCode(304, uce.Message);
            }
        }

        /// <summary>
        /// Rejestracja użytkownika w bazie danych
        /// </summary>
        /// <param name="userCreateDTO">JSON zawierający wszystkie wymagane dane użytkownika</param>
        /// <returns>JSON ze wszystkimi danymi nowego użytkownika</returns>
        /// <response code="201">Poprawnie utworzono użytkownika - zwraca token JWT</response>
        /// <response code="400">Przekazano niepoprawne zapytanie</response>
        [ProducesResponseType(typeof(UserTokenDTO), 201), ProducesResponseType(400)]
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

            var user = await this._userManager.FindByNameAsync(userCreateDTO.Email);
            var userToken = new UserTokenDTO
            {
                Token = this._oAuthService.GetUserAuthToken(userCreateDTO.Email, user.Id.ToString())
            };

            return StatusCode((int)HttpStatusCode.Created, userToken);
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

            if (!ModelState.IsValid || id == 0) // Użytkownik System
                return BadRequest(ModelState);

            var isUserExists = await _userService.IsUserExistsAsync(id);
            if (!isUserExists)
                return NotFound();

            try
            {
                await _userService.DeleteUserAsync(id);
                _logger.Info(string.Format("User with id={0} has been deleted.", id));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during User DELETE operation");
                return StatusCode(500);
            }
        }

        public class UserControllerException : ApplicationException
        {
            public UserControllerException(string message) : base(message) { }
        }
    }
}