using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class RoomFacilityViewComponent:ViewComponent
    {
        private HotelCloudDbContext _context;

        public RoomFacilityViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            RoomFacilityViewModel model = new RoomFacilityViewModel();
            if(id == 0)
            {

            }

            var roomfacilities = _context.RoomFacilities.
                Include(p => p.HotelRoom).FirstOrDefault(p => p.HotelRoom.HotelRoomId == id);

            if(roomfacilities != null)
            {
                model.Ac = roomfacilities.Ac;
                model.Tv = roomfacilities.Tv;
                model.AttachedWashRoom = roomfacilities.AttachedWashRoom;
                model.Internet = roomfacilities.Internet;
            }
            return View(model);
        }
        }
}
