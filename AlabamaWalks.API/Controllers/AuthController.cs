using Microsoft.AspNetCore.Mvc;

namespace AlabamaWalks.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
