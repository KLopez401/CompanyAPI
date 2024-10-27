using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.Auth;

namespace CompanyApp.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public LoginService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        public async Task<string> Login(AuthRequest loginRequest)
        {
            var user = await _userService.GetUserByUsername(loginRequest.Username);
            var verifyPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);
            if (user == null || !verifyPassword)
            {
                throw new Exception("Invalid credentials.");
            }

            var role = await _userService.GetUserRole(user.UserId);

            return _jwtService.GenerateJwtToken(user, role);
        }
    }
}
