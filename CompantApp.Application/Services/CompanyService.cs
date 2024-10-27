using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.Company;
using CompanyApp.Domain.Repository.Interfaces;

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

        public async Task<CompanyDto> GetCompanyByUserId(Guid userId)
        {
            var data = await _companyRepository.GetCompanyByUserId(userId);

            var company = new CompanyDto()
            {
                Id = data.Id,
                Name = data.Name,
            };

            return company;
        }

        public async Task<IEnumerable<CompanyUsersDto>> GetCompanyUsers(int id)
        {
            var response = new List<CompanyUsersDto>();

            var companyUsers = await _companyRepository.GetCompanyUsers(id);
            foreach (var user in companyUsers)
            {
                response.Add(new CompanyUsersDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    CompanyName = user.CompanyName,
                });
            }

            return response;
        }
    }
}
