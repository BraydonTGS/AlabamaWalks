﻿namespace AlabamaWalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Many to Many Relationship //
        // Navigation Property //
       public List<User_Role> UserRoles { get; set; }

        
    }
}
