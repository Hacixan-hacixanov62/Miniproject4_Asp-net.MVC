using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<SliderVM> Sliders { get; set; }
        public IEnumerable<InformationVM> Informations { get; set; }
        public IEnumerable<AboutVM> Abouts { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
    }
}
