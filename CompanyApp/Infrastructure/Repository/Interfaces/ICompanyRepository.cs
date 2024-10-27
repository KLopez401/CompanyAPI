using CompanyApp.Domain.Model.Company;

namespace CompanyApp.Infrastructure.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyById(int id);
    }
}
