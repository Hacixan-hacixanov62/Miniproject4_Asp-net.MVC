using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Services;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Instructors;
using System.Reflection.Metadata;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IInstructorService _instructorService;
        private readonly IWebHostEnvironment _env;
        public InstructorController(AppDbContext context,
                                    IInstructorService instructorService,
                                    IWebHostEnvironment env)
        {
            
            _context = context;
            _instructorService = instructorService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _instructorService.GetAllAsync());
        }


        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorCreateVM request)
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

            bool existSlider = await _instructorService.ExistAsync(request.Email);

            //if (existSlider)
            //{
            //    ModelState.AddModelError("Title", "Slider with this title or description already exists");
            //    ModelState.AddModelError("Description", "Slider with this title or description already exists");
            //    return View();
            //}

            await _instructorService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var instruc = await _instructorService.GetByIdAsync((int)id);

            if (instruc is null) return NotFound();

            await _instructorService.DeleteAsync(instruc.Id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var instruc = await _instructorService.GetByIdAsync((int)id);

            if (instruc is null) return NotFound();

            return View(new InstructorDetialVM
            {
                FullName = instruc.FullName,
                Field  = instruc.Field,
                Address = instruc.Address,
                Email = instruc.Email,
                Image = instruc.Image,
                CreatedDate = instruc.CreatedDate.ToString("MM.dd.yyyy")
            });
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var instruc = await _context.Instructors.FindAsync(id);

            if (instruc is null) return NotFound();

            return View(new InstructorEditVM
            {
             
                FullName = instruc.FullName,
                Field  = instruc.Field,
                Address = instruc.Address,
                Email = instruc.Email , 
                Image = instruc.Image
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,InstructorEditVM request)
        {
            if (id is null) return BadRequest();

            var instruc = await _context.Instructors.FindAsync(id);

            if (instruc is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    request.Image = instruc.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                    request.Image = instruc.Image;
                    return View(request);
                }

                string oldPath = _env.GenerateFilePath("img", instruc.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);
                instruc.Image = fileName;
            }

            instruc.FullName = request.FullName;
            instruc.Address = request.Address;
            instruc.Email = request.Email;
            instruc.Field = request.Field;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
