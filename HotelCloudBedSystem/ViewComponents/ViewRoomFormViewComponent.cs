using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class ViewRoomFormViewComponent:ViewComponent
    {
        public ViewRoomFormViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(int hotelId,int RoomTypeId
            , DateTime CheckInDate, DateTime CheckOutDate)
        {

            RoomSearchViewModel model = null;

            model = new RoomSearchViewModel()
            {
                HotelId=hotelId,
                hotelRoomTypeId=RoomTypeId,
                CheckInDate = CheckInDate,
                CheckOutDate = CheckOutDate

            };
            return View(model);
        }
        }
}
