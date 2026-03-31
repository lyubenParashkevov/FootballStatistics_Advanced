using System.Diagnostics;
using FootballStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Error404()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error500()
        {
            return View();
        }

        
    }
}
