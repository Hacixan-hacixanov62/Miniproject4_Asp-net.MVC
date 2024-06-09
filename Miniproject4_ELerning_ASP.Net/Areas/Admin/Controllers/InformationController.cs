 using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InformationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IInformationService _informationService;
        private readonly IWebHostEnvironment _env;
        public InformationController(AppDbContext context,
                                    IInformationService informationService,
                                    IWebHostEnvironment env)
        {
            _context = context;
            _informationService = informationService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _informationService.GetAllAsync());
        }


        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationCreateVM request)
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

            bool existSlider = await _informationService.ExistAsync(request.Title, request.Description);

            //if (existSlider)
            //{
            //    ModelState.AddModelError("Title", "Slider with this title or description already exists");
            //    ModelState.AddModelError("Description", "Slider with this title or description already exists");
            //    return View();
            //}

            await _informationService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _informationService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            await _informationService.DeleteAsync(blog.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _informationService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            return View(new InformationDetailVM
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

            var ınformation = await _context.Informations.FindAsync(id);

            if (ınformation is null) return NotFound();

            var viewModel = new InformationEditVM
            {
                Image = ınformation.Image,
                Title = ınformation.Title,
                Description = ınformation.Description

            };

            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, InformationEditVM request)
        {
            if (id is null) return BadRequest();

            var information = await _context.Informations.FindAsync(id);

            if (information is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    request.Image = information.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                    request.Image = information.Image;
                    return View(request);
                }

                string oldPath = _env.GenerateFilePath("img", information.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);
                information.Image = fileName;
            }

            information.Title = request.Title;
            information.Description = request.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}