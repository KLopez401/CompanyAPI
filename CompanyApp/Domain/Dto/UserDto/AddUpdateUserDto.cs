using CompanyApp.Application.Helper.Enum;

namespace CompanyApp.Domain.Dto.UserDto
{
    public class AddUpdateUserDto : BaseUser
    {
        public int CompanyId { get; set; }
        public string Password { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }
    }
}
