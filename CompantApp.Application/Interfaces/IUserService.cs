using CompanyApp.Domain.Dto.UserDto;

namespace CompanyApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<IEnumerable<UserDto>> GetAllForUserAccount(int companyId);
        Task<GetUserDto> GetUserByUsername(string username);
        Task<bool> CheckExistIfUserName(string username, Guid userId);
        Task<bool> CheckUserCompany(int id, string userName);
        Task<string> GetUserRole(Guid userId);
        Task<bool> Add(AddUpdateUserDto model);
        Task<bool> Update(AddUpdateUserDto model, Guid userId);
        Task<bool> Delete(Guid id);
    }
}
