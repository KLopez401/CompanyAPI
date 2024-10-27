using CompanyApp.Domain.Dto.Role;

namespace CompanyApp.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDto> GetRoleById(int id);
    }
}
