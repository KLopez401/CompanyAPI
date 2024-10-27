using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.UserDto;

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

        public async Task<string> Login(GetUserDto user)
        {
            var role = await _userService.GetUserRole(user.UserId);

            return _jwtService.GenerateJwtToken(user, role);
        }

        public Task<bool> VerifyHashPassword(string password, string hashPassword)
        {
            var verify = BCrypt.Net.BCrypt.Verify(password, hashPassword);

            return Task.FromResult(verify);
        }
    }
}
