using CompanyApp.Domain.Model.Roles;

namespace CompanyApp.Domain.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<Roles> GetRoleById(int id);
        Task<Roles> GetRoleByName(string roleName);
    }
}
