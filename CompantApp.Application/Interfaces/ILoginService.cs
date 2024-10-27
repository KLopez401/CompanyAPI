using CompanyApp.Domain.Dto.Auth;

namespace CompanyApp.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(AuthRequest loginRequest);
    }
}
