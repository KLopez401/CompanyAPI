namespace CompanyApp.Domain.Dto.UserDto
{
    public class GetUserDto : UserDto
    {
        public Guid UserId { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public int CompanyId { get; set; }
    }
}
