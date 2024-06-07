using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Models;
using System.Diagnostics;

namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
