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

        [HttpPost( "Login" ), AllowAnonymous]
        public async Task<IActionResult> LoginUserAsync(UserLoginDTO userLoginDto)
        {

            SignInResult result = await this._signInManager.PasswordSignInAsync( userLoginDto.Username, userLoginDto.Password, false, false );
            if(result.IsLockedOut)
            {
                return BadRequest( "User Account is locked out!" );
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