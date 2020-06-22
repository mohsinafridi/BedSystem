using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class RoomReservationButtonFormViewComponent:ViewComponent
    {

        public RoomReservationButtonFormViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(int roomId
            , DateTime CheckInDate, DateTime CheckOutDate)
        {

            RoomReservationViewModel model = new RoomReservationViewModel()
            {
                RoomId = roomId,
                ChkIndate = CheckInDate,
                ChkOutdate = CheckOutDate

            };
            return View(model);
        }
    }
}
