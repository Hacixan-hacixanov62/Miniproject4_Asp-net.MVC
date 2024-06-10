using Microsoft.AspNetCore.Mvc;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
