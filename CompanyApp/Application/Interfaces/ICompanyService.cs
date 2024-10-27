using CompanyApp.Domain.Dto.Company;

namespace CompanyApp.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompanyById(int id);
    }
}
