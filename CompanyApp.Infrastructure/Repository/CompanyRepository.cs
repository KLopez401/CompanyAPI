using CompanyApp.Domain.Model.Company;
using CompanyApp.Domain.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompanyApp.Infrastructure.Repository
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

        public async Task<Company> GetCompanyByUserId(Guid userId)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            var company = connection.QueryFirstOrDefaultAsync<Company>(
                "GetCompanyByUserId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await company;
        }

        public async Task<IEnumerable<CompanyUsers>> GetCompanyUsers(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", id);

            var users = connection.QueryAsync<CompanyUsers>(
                "GetCompanyUsers",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await users;
        }
    }
}
