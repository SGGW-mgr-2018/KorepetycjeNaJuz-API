using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOAuthService _oAuthService;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, IOAuthService oAuthService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._oAuthService = oAuthService;
        }

        /// <summary>
        /// Asynchroniczna metoda logująca.
        /// </summary>
        /// <param name="userLoginDto">Dane logowania</param>
        /// <returns>Wygenerowany token JWT</returns>
        /// <response code="200">Pomyślna autoryzacja użytkownika - zwraca wygenerowany JWT Token</response>
        /// <response code="400">Logowanie nie powiodło się - niepoprawne dane logowania</response>
        /// <response code="403">Konto użytkownika jest zablokowane</response>
        [HttpPost( "Login" ), AllowAnonymous]
        public async Task<IActionResult> LoginUserAsync([Required] UserLoginDTO userLoginDto)
        {

            SignInResult result = await this._signInManager.PasswordSignInAsync( userLoginDto.Username, userLoginDto.Password, false, false );
            if(result.IsLockedOut)
            {
                return Forbid( "User Account is locked out!" );
            }
            if(!result.Succeeded)
            {
                return BadRequest( "Sign in failed!" );
            }
            User user = await this._userManager.FindByNameAsync( userLoginDto.Username );
            string userToken = this._oAuthService.GetUserAuthToken( userLoginDto.Username, user.Id.ToString() );
            return new JsonResult( new { token = userToken } );
        }

        /// <summary>
        /// Synchroniczna metoda zwracająca nowy token (z nowym Expiration Date).
        /// </summary>
        /// <returns>Wygenerowany token JWT</returns>
        /// <response code="200">Pomyślnie odświeżono token użytkownika - zwraca wygenerowany JWT Token</response>
        /// <response code="401">Nie podano aktualnego tokenu. Użytkownik nie zalogowany.</response>
        [HttpPost("Refresh"), Authorize("Bearer")]
        public IActionResult RefreshToken()
        {
            string token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
            System.Collections.Generic.IDictionary<string, string> claims = this._oAuthService.GetClaims(token);
            string newToken = this._oAuthService.GetUserAuthToken(claims["unique_name"], claims["UserId"]);
            return new JsonResult(new { token = newToken });
        }

    }
}