namespace AlabamaWalks.API.Models.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }
        // Navigation Property to the User Table //
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
