namespace CompanyApp.Domain.Model.Users
{
    public class Users : BaseUsers
    {
        public Guid UserID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
