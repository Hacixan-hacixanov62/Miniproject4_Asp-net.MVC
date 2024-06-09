using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Services;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context,
                                  IWebHostEnvironment env,
                                  ICategoryService categoryService)
        {        
            _context = context;
            _env = env;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _categoryService.GetAllAsync());
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
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

            bool existSlider = await _categoryService.ExistAsync(request.Name);

            //if (existSlider)
            //{
            //    ModelState.AddModelError("Name", "Slider with this name or description already exists");
            //    return View();
            //}

            await _categoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var name = await _categoryService.GetByIdAsync((int)id);

            if (name is null) return NotFound();

            await _categoryService.DeleteAsync(name.Id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var name = await _categoryService.GetByIdAsync((int)id);

            if (name is null) return NotFound();

            return View(new CategoryDetialVM
            {
                Name = name.Name,
                Image = name.Image,
                CreatedDate = name.CreatedDate,           
            });
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var name = await _context.Categories.FindAsync(id);

            if (name is null) return NotFound();

            return View(new CategoryEditVM
            {
                Name = name.Name,
                Image = name.Image
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            var name = await _context.Categories.FindAsync(id);

            if (name is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    request.Image = name.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                    request.Image = name.Image;
                    return View(request);
                }

                string oldPath = _env.GenerateFilePath("img", name.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);
                name.Image = fileName;
            }

             name.Name = request.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }



}

