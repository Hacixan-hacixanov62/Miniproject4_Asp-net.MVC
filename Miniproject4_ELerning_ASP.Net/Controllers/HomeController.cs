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

        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync()
            };

            return View(model); 
           
        }

        

    }
}
