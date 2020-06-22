using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class MainHotelSearchViewComponent:ViewComponent
    {
        private HotelCloudDbContext _context;

        public MainHotelSearchViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            HotelSerachViewModel model = null;
            var roomtypes = _context.hotelRoomTypes;

            if (roomtypes != null)
            {
                model = new HotelSerachViewModel()
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
