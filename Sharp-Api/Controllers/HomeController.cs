using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyApi.Controllers
{
    [Route("[controller]/[action]")]

    public class HomeController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public HomeController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogError("test");
            return View();
        }
    }
}
