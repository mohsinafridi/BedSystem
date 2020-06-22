using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewComponents
{
    public class RoomTypeViewComponent:ViewComponent
    {
        private HotelCloudDbContext _context;

        public RoomTypeViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<RoomTypeViewModel> list = new List<RoomTypeViewModel>();
            RoomCreateViewModel model = null;
            var roomtypes = _context.hotelRoomTypes;

            if (roomtypes != null)
            {
                model = new RoomCreateViewModel()
                {
                    Roomtypes = roomtypes.Select(p => new SelectListItem()
                    {
                        Text = p.RoomType,
                        Value = p.HotelRoomTypeId.ToString()
                    }).ToList(),

                };
            }
           

            return View(model);
        }
    }
}
