using CompanyApp.Domain.Model.Company;
using CompanyApp.Infrastructure.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompanyApp.Infrastructure.Repository.Implementation
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public CompanyRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<Company> GetCompanyById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", id);

            var company = connection.QueryFirstOrDefaultAsync<Company>(
                "GetCompanyById",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await company;
        }
    }
}
