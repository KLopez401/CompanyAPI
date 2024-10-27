using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.UserDto;
using CompanyApp.Domain.Model.Users;
using CompanyApp.Domain.Repository.Interfaces;

namespace CompanyApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _configuration;

        public UserService(
            IUserRepository userRepository, 
            ICompanyRepository companyRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _configuration = configuration;
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var response = new List<UserDto>();

            var users = await _userRepository.GetAll();

            response.AddRange(from user in users
                              select new UserDto()
                              {
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Username = user.Username,
                                  RoleName = user.RoleName,
                                  CompanyName = user.CompanyName,
                              });

            return response;
        }

        public async Task<IEnumerable<UserDto>> GetAllForUserAccount(int companyId)
        {
            var response = new List<UserDto>();

            var users = await _userRepository.GetAllForUserAccount(companyId);
            foreach (var user in users)
            {
                response.Add(new UserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    RoleName = user.RoleName,
                    CompanyName = user.CompanyName,
                });
            }

            return response;
        }

        public async Task<UserDto> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return null;

            var response = new UserDto();
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Username = user.Username;
            response.RoleName = user.RoleName;
            response.CompanyName = user.CompanyName;

            return response;
        }

        public async Task<GetUserDto> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null) return null;

            var response = new GetUserDto();
            response.UserId = user.Id;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Username = user.Username;
            response.PasswordHash = user.PasswordHash;
            response.RoleID = user.RoleID;
            response.RoleName = user.RoleName;
            response.CompanyId = user.CompanyId;
            response.CompanyName = user.CompanyName;

            return response;
        }

        public async Task<bool> CheckExistIfUserName(string username, Guid userId)
        {
            var user = await GetUserByUsername(username);

            if (user != null && user.UserId != userId)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckUserCompany(int id, string userName)
        {
            var data = await GetUserByUsername(userName);
            if (data.CompanyId == id) return true;

            return false;
        }

        public async Task<string> GetUserRole(Guid userId)
        {
            var user = await _userRepository.GetUserRoles(userId);
            if (user == null)
            {
                throw new Exception("No user found.");
            }

            return user;
        }
        public async Task<bool> Add(AddUpdateUserDto model)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new AddUpdateUsers
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = passwordHash,
                RoleID = (int)model.Role,
                CompanyId = model.CompanyId,
            };

            try
            {
                await _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> Update(AddUpdateUserDto model, Guid userId)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new AddUpdateUsers
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = passwordHash,
                RoleID = (int)model.Role,
                CompanyId = model.CompanyId,
            };

            try
            {
                await _userRepository.Update(user, userId);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
