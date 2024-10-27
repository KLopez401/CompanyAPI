namespace CompanyApp.Domain.Dto.UserDto
{
    public class UserDto : BaseUser
    {
        public Guid UserID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
