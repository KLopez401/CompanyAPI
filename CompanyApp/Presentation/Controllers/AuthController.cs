using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Presentation.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        /// <summary>
        /// initialize ILoginService
        /// Initialize IUserService
        /// </summary>
        /// <param name="loginService"></param>
        /// <param name="userService"></param>
        public AuthController(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }
        /// <summary>
        /// Use for login request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>jwt token</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            try
            {
                var user = await _userService.GetUserByUsername(request.Username);
                if (user == null)
                {
                    return Unauthorized("This account is unauthorized or user accounts can't be found");
                }

                var verifyPassword = _loginService.VerifyHashPassword(request.Password, user.PasswordHash);
                if (user == null || !verifyPassword.Result)
                {
                    return Unauthorized("Invalid credentials.");
                }

                var token = await _loginService.Login(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
