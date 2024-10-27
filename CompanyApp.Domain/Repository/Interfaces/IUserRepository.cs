using CompanyApp.Domain.Model.Users;

namespace CompanyApp.Domain.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetById(Guid id);
        Task<IEnumerable<Users>> GetAll();
        Task<GetUser> GetUserByUsername(string username);
        Task<string> GetUserRoles(Guid userId);
        Task<IEnumerable<Users>> GetAllForUserAccount(int companyId);
        Task Add(AddUpdateUsers model);
        Task Update(AddUpdateUsers model, Guid id);
        Task Delete(Guid id);
    }
}
