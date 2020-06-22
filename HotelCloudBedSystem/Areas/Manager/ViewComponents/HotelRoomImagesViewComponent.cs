using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewComponents
{
    public class HotelRoomImagesViewComponent:ViewComponent
    {
        private HotelCloudDbContext _context;

        public HotelRoomImagesViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            
            HotelRoomImagesViewModel model = null;
            List<HotelRoomImagesViewModel> list = new List<HotelRoomImagesViewModel>();

            if(id == 0)
            {

            }

            var hotelroomimages = _context.roomsImages.Where(p=>p.HotelRoom.HotelRoomId ==id)
                .ToList();

            if(hotelroomimages == null)
            {

            }

           foreach(var image in hotelroomimages)
            {
                model = new HotelRoomImagesViewModel()
                {
                    HotelRoomimage=image.images
                };

                list.Add(model);
            }
            return View(list);
        }
        }
}
