using CompanyApp.Domain.Model.Roles;

namespace CompanyApp.Infrastructure.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<Roles> GetRoleById(int id);
    }
}
