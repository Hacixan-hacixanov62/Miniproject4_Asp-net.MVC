using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using System.Security.AccessControl;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISocialService _socialService;
        public SocialController(AppDbContext context,
                                IWebHostEnvironment env,
                                 ISocialService socialService)
        {
            _context = context;
            _env = env;
            _socialService = socialService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _socialService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Social request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existSocial = await _socialService.ExistAsync(request.Name);
            if (existSocial)
            {
                ModelState.AddModelError("Name", "This name already exist");
                return View();
            }
            await _socialService.CreateAsync(new Social { Name = request.Name });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var deleteSocial = await _socialService.GetByIdAsync((int)id);
            if (deleteSocial is null) return NotFound();

            _socialService.DeleteAsync(deleteSocial);
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var social = await _socialService.GetByIdAsync((int)id);
            if (social is null) return NotFound();


            return View(new Social { Name = social.Name});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Social request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();
            var social = await _socialService.GetByIdAsync((int) id);
            if (social is null) return NotFound();

            if(request.Name is not null)
            {
                social.Name = request.Name;
            }

            await _socialService.EditAsync();
            return RedirectToAction(nameof(Index));

        }

    

    }
}
