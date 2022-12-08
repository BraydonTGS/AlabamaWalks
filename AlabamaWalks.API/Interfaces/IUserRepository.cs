using Microsoft.IdentityModel.Tokens;

namespace AlabamaWalks.API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUserAsync(string username, string password); 
    }
}
