using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class MainLogoViewComponent:ViewComponent
    {
        public MainLogoViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
