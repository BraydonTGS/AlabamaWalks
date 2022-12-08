using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AlabamaWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;

        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {   // Validation Set using Fluent Validations // 

            // Check if user is authenticated //
            var response =  await _repository.AuthenticateUserAsync(request.UserName, request.Password);
            if (response)
            {
                // Generate Jwt Token //
            }

            return BadRequest("Invalid Username or Password"); 
        }
    }
}
