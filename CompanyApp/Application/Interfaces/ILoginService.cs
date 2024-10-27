using CompanyApp.Domain.Dto.UserDto;

namespace CompanyApp.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(GetUserDto user);
        Task<bool> VerifyHashPassword(string password, string hashPassword);
    }
}
