using CompanyApp.Domain.Model.Users;
using CompanyApp.Domain.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompanyApp.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<GetUser> GetUserByUsername(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", username);

            var user = connection.QueryFirstOrDefaultAsync<GetUser>(
                "GetUserByUserName",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await user;
        }
        public async Task<Users> GetById(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var user = connection.QueryFirstOrDefaultAsync<Users>(
                "GetUserById",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await user;
        }
        public async Task<string> GetUserRoles(Guid userId)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            var user = connection.QueryFirstOrDefaultAsync<string>(
                "GerUserRoles",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await user;
        }
        public async Task<IEnumerable<Users>> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);

            var users = connection.QueryAsync<Users>(
                "GetAllUser",
                commandType: CommandType.StoredProcedure
            );

            return await users;
        }
        public async Task<IEnumerable<Users>> GetAllForUserAccount(int companyId)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            var users = connection.QueryAsync<Users>(
                "GetAllForUsers",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await users;
        }
        public async Task Add(AddUpdateUsers model)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", model.FirstName);
            parameters.Add("@LastName", model.LastName);
            parameters.Add("@UserName", model.Username);
            parameters.Add("@PasswordHash", model.PasswordHash);
            parameters.Add("@RoleID", model.RoleID);
            parameters.Add("@CompanyId", model.CompanyId);

            await connection.ExecuteAsync(
                "AddUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task Update(AddUpdateUsers model, Guid userID)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID);
            parameters.Add("@FirstName", model.FirstName);
            parameters.Add("@LastName", model.LastName);
            parameters.Add("@UserName", model.Username);
            parameters.Add("@PasswordHash", model.PasswordHash);
            parameters.Add("@RoleID", model.RoleID);
            parameters.Add("@CompanyId", model.CompanyId);

            await connection.ExecuteAsync(
                "UpdateUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task Delete(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@UserID", id);

            await connection.ExecuteAsync(
                "DeleteUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
