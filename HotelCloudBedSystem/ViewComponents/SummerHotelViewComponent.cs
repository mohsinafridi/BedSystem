
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class SummerHotelViewComponent : ViewComponent
    {
        private HotelCloudDbContext _context;
        public SummerHotelViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            HotelListViewModel model = null;
            List<HotelListViewModel> list = new List<HotelListViewModel>();
            var hotels =  _context.hotels.Where(p => p.HotelCity == "Lahore" || p.HotelCity == "Karachi"
              || p.HotelCity == "Peshawer" || p.HotelCity == "Mardan" || p.HotelCity == "Hyderabad" ||
              p.HotelCity == "Sialkot" || p.HotelCity == "FaisalAbad" || p.HotelCity == "Rawalpindi"
              ||p.HotelCity =="Nawshehra" || p.HotelCity =="Islamabad")
                .ToList();

            if(hotels == null)
            {

            }

            foreach(var hotel in hotels)
            {
                model = new HotelListViewModel()
                {
                    HotelName = hotel.HotelName,
                    HotelCity = hotel.HotelCity,
                    NoOfRooms = hotel.NoOfRooms,
                    HotelImage=hotel.HotelImage
                };

                list.Add(model);
            }
            return View(list);

        }

    }
}
