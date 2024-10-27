using CompanyApp.Application.Helper.Enum;

namespace CompanyApp.Domain.Dto.UserDto
{
    public class UserDto : BaseUser
    {
        public string CompanyName { get; set; } = string.Empty;
        public string RoleName { get; set; }
    }
}
