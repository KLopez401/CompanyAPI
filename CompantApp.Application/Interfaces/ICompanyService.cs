using CompanyApp.Domain.Dto.Company;

namespace CompanyApp.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyUsersDto>> GetCompanyUsers(int id);
        Task<CompanyDto> GetCompanyById(int id);
        Task<CompanyDto> GetCompanyByUserId(Guid userId);
    }
}
