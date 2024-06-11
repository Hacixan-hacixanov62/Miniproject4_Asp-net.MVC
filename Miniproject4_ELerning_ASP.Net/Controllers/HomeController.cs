using Microsoft.AspNetCore.Mvc;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels;
using System.Diagnostics;

namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IInformationService _informationService;
        private readonly IAboutService _aboutService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;
        private readonly ICourseServicecs _courseServicecs;
        private readonly IStudentService _studentService;

        public HomeController(ISliderService sliderService,
                              IInformationService ınformationService,
                              IAboutService aboutService,
                              ICategoryService categoryService,
                              IInstructorService instructorService,
                              ICourseServicecs courseServicecs,
                              IStudentService studentService)
        {
            _sliderService = sliderService;
            _informationService = ınformationService;
            _aboutService = aboutService;
            _categoryService = categoryService;
            _instructorService = instructorService;
            _courseServicecs = courseServicecs;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Informations = await _informationService.GetAllAsync(),
                Abouts = await _aboutService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Instructors = await _instructorService.GetAllAsync(),
                Courses = await _courseServicecs.GetAllAsync(),
                Students = await _studentService.GetAllAsync()
            };

            return View(model); 
           
        }

        

    }
}
