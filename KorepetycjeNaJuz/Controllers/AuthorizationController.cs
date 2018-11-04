using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace KorepetycjeNaJuz.Controllers
{
    [Authorize( "Bearer" )]
    [Route( "api/[controller]" )]
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
        /// Asynchroniczna metoda logująca. Dla poprawnych danych logowania, zwracany jest kod 200 oraz token typu JWT.
        /// Kody błędu: 400 Bad Request (Niepoprawne dane logowania); 403 Forbidden (konto zablokowane)
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>
        /// Kody: 200 OK, 400 Bad Request (Złe dane logowania), 403 Forbidden (konto zablokowane)
        /// Dla kodu 200 OK: dodatkowo token JWT
        /// </returns>
        [HttpPost( "Login" ), AllowAnonymous]        
        public async Task<IActionResult> LoginUserAsync(UserLoginDTO userLoginDto)
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
            return new JsonResult( userToken );
        }


    }
}