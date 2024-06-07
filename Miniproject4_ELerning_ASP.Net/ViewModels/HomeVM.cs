using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<SliderVM> Sliders { get; set; }
        public IEnumerable<InformationVM> Informations { get; set; }
    }
}
