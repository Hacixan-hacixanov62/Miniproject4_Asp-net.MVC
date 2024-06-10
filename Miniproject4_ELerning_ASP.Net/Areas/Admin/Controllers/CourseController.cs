using Miniproject4_ELerning_ASP_MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Course;
using System.Xml.Linq;

namespace Miniproject4_ELerning_ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICourseServicecs _courseService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;
        public CourseController(AppDbContext context,
                                ICourseServicecs courseServicecs,
                                IWebHostEnvironment env,
                                ICategoryService categoryService,
                                IInstructorService instructorService)
        {
            _context = context;
            _courseService = courseServicecs;
            _env = env;
            _categoryService = categoryService;
            _instructorService = instructorService;


        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _courseService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _courseService.GetMappedDatas(courses);

            int totalPage = await GetPageCountAsync(4);

            Paginate<CourseAdminVM> response = new(mappedDatas, totalPage, page);

            return View(response);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _courseService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            ViewBag.instructors = await _instructorService.GetAllSelectedAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            ViewBag.instructors = await _instructorService.GetAllSelectedAsync();


            if (!ModelState.IsValid)
            {
                 return View();
            }


            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size can be max 500 Kb");
                    ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                    return View();
                }
            }

             await _courseService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            return View(new CourseDetialVM
            {
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Instructor = course.Instructor.FullName,
                Category = course.Category.Name,
                Rating = course.Rating,
                StartDate = course.StartDate.ToString("MM.dd.yyyy"),
                EndDate = course.EndDate.ToString("MM.dd.yyyy"),
                CourseImages = course.CourseImages.Select(i => new CourseImageVM
                {
                
                    Name = i.Name,
                    IsMain = i.IsMain
                }).ToList()
            });
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
            ViewBag.instructors = await _instructorService.GetAllSelectedAsync();

            return View(new CourseEditVM
            {
                Name = course.Name,
                Description = course.Description,
                Price = course.Price.ToString(),
                CategoryId = course.CategoryId,
                InstructorId = course.InstructorId,
                Rating = course.Rating,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Images = course.CourseImages.Select(m => new CourseImageEditVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsMain = m.IsMain,
                    CourseId = m.CourseId

                }).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM request)
        {
            ViewBag.instructors = await _instructorService.GetAllSelectedAsync();
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            List<CourseImageEditVM> images = course.CourseImages
                .Select(m => new CourseImageEditVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsMain = m.IsMain,
                })
                .ToList();

            request.Images = images;

            if (!ModelState.IsValid)
            {
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (request.Price is not null)
            {
                if (decimal.Parse(request.Price) <= decimal.Parse(request.Price))
                {
                    ModelState.AddModelError("Price", " price must be smaller than price");
                    ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                    return View(request);
                }
            }

            if (request.NewImages is not null)
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Images", "Image size can be max 500 Kb");
                        ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                        return View(request);
                    }

                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Images", "File type must be only image");
                        ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                        return View(request);
                    }
                }
            }

            await _courseService.EditAsync(course, request);

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> DeleteCourseImage(MainAndDeleteImageVM request)
        //{
        //    await _courseService.DeleteCourseImageAsync(request);
        //    return Ok();
        //}


        //[HttpPost]
        //public async Task<IActionResult> SetMainImage(MainAndDeleteImageVM request)
        //{
        //    await _courseService.SetMainImageAsync(request);

        //    return Ok();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            await _courseService.DeleteAsync(course);

            return RedirectToAction(nameof(Index));
        }



    }
}
