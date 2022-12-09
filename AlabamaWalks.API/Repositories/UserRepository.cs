using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlabamaWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlabamaWalksDbContext _context;

        public UserRepository(AlabamaWalksDbContext context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password); 

            if (user == null)
            {
                return null; 
            }
            var userRoles = await _context.Users_Roles.Where(x => x.UserId== user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>(); 
                foreach (var role in userRoles)
                {
                    var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == role.RoleId);
                    if (userRole != null)
                    {
                        user.Roles.Add(userRole.Name); 
                    }

                }
            }

            user.Password = null;
            return user; 
        }
    }
}
