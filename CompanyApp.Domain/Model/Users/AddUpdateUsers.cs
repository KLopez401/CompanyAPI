namespace CompanyApp.Domain.Model.Users
{
    public class AddUpdateUsers : BaseUsers
    {
        public int CompanyId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleID { get; set; }
    }
}
