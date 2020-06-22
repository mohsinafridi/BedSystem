using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class HotelProfileViewComponent : ViewComponent
    {
        private HotelCloudDbContext _context;

        public HotelProfileViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = new HotelProfileViewModel();

            var hotel = _context.hotels.FirstOrDefault(p => p.HotelId == id);
            if (hotel != null)
            {
                model.HotelId = hotel.HotelId;
                model.HotelName = hotel.HotelName;
                model.NoOfFloors = hotel.NoOfFloors;
                model.NoOfRooms = hotel.NoOfRooms;
                model.HotelCity = hotel.HotelCity;
                model.Address = hotel.Address;
                model.HotelImage = hotel.HotelImage;
                model.Description = hotel.Description;


            }

            return View(model);


        }
    }
}
