using Microsoft.AspNetCore.Mvc;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
