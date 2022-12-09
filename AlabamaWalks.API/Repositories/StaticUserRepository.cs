using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Braydon",
                LastName = "Sutherland",
                EmailAddress = "BraydonTGS@gmail.com",
                Id = Guid.NewGuid(),
                UserName = "GeoMatix",
                Password = "JumpUp123",
                Roles = new List<string>{"reader", "writer"}
                // The user with reader/writer will be able to access all 5 endpoints //
            }
        };

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = Users.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);
            return user; 
        }
    }
}
