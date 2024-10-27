using CompanyApp.Domain.Model.Company;

namespace CompanyApp.Domain.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyUsers>> GetCompanyUsers(int id);
        Task<Company> GetCompanyById(int id);
        Task<Company> GetCompanyByUserId(Guid userId);
    }
}
