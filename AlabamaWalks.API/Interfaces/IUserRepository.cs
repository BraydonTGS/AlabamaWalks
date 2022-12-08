using AlabamaWalks.API.Models.Domain;
using Microsoft.IdentityModel.Tokens;

namespace AlabamaWalks.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string username, string password); 
    }
}
