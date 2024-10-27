using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.Company;
using CompanyApp.Infrastructure.Repository.Interfaces;

namespace CompanyApp.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _configuration;

        public CompanyService(ICompanyRepository companyRepository, IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _configuration = configuration;
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            var data = await _companyRepository.GetCompanyById(id);
            if (data == null) return null;

            var company = new CompanyDto()
            {
                Id = data.Id,
                Name = data.Name,
            };

            return company;
        }
    }
}
