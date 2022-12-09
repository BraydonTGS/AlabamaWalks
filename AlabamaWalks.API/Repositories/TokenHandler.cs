using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlabamaWalks.API.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            // Create Claims //
            var claims = new List<Claim>(); 
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            // Loop into roles of users //
     /*       user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            });*/

            // Takes in a Validated User and Returns a JWT //
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // Create Credentials which we will pass to a Jwt Security Token //
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Creating the Token //
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials : credentials
                ); 

            // Makes Sure we have a String Token from all the information that we have passed //
           return Task.FromResult( new JwtSecurityTokenHandler().WriteToken(token));


        }
    }
}
