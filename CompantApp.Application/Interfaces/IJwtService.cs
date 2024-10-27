using CompanyApp.Domain.Dto.UserDto;

namespace CompanyApp.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(GetUserDto user, string role);
    }
}
