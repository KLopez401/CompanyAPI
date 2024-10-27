namespace CompanyApp.Domain.Model.Users
{
    public class GetUser : Users
    {
        public Guid Id { get; set; }
        public int RoleID { get; set; }
        public int CompanyId { get; set; }
        public string PasswordHash { get; set; }
    }
}
