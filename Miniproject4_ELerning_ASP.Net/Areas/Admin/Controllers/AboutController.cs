using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Services;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context,
                               IAboutService aboutService,
                                IWebHostEnvironment env)
        {
            _context = context;
            _aboutService = aboutService;
            _env = env;
            
        }

        public async Task<IActionResult> Index()
        {
            return View(await _aboutService.GetAllAsync());
        }


        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }

            if (!request.Image.CheckFileSize(1024))
            {
                ModelState.AddModelError("Image", "Image size must be max 1024 KB");
                return View();
            }

            bool existSlider = await _aboutService.ExistAsync(request.Title, request.Description);

            //if (existSlider)
            //{
            //    ModelState.AddModelError("Title", "Slider with this title or description already exists");
            //    ModelState.AddModelError("Description", "Slider with this title or description already exists");
            //    return View();
            //}

            await _aboutService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _aboutService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            await _aboutService.DeleteAsync(blog.Id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _aboutService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            return View(new AboutDetialVM
            {
                Title = blog.Title,
                Description = blog.Description,
                CreatedDate = blog.CreatedDate,
                Image = blog.Image
            });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var about = await _context.Abouts.FindAsync(id);

            if (about is null) return NotFound();

            return View(new AboutEditVM {
                Image = about.Image,
                Title = about.Title,
                Description = about.Description,
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id is null) return BadRequest();

            var about = await _context.Abouts.FindAsync(id);

            if (about is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    request.Image = about.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                    request.Image = about.Image;
                    return View(request);
                }

                string oldPath = _env.GenerateFilePath("img", about.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);
                about.Image = fileName;
            }

            about.Title = request.Title;
            about.Description = request.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }



    
}
