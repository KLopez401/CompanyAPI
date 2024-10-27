using CompanyApp.Domain.Model.Roles;
using CompanyApp.Infrastructure.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompanyApp.Infrastructure.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public RoleRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<Roles> GetRoleById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var roles = connection.QueryFirstOrDefaultAsync<Roles>(
                "GetRoleById",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await roles;
        }
    }
}
