using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewComponents
{
    public class HotelImagesViewComponent:ViewComponent
    {
        private HotelCloudDbContext _context;

        public HotelImagesViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            
            HotelImagesViewModel model = null;
            List<HotelImagesViewModel> list = new List<HotelImagesViewModel>();

            if(id == 0)
            {

            }

            var hotelimages = _context.hotelImages.Where(p=>p.Hotel.HotelId ==id)
                .ToList();

            if(hotelimages == null)
            {

            }

           foreach(var image in hotelimages)
            {
                model = new HotelImagesViewModel()
                {
                    Hotelimage=image.HImage
                };

                list.Add(model);
            }
            return View(list);
        }
        }
}
