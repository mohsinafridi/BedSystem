using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class HotelListViewComponent : ViewComponent
    {

        private IEFRepository _repository;

        public HotelListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<HotelListViewModel> listmodel = new List<HotelListViewModel>();

            var hotels = _repository.GetHotels();

            if (hotels != null)
            {
                listmodel = hotels.Select(p => new HotelListViewModel()
                {
                    HotelName = p.HotelName,
                    NoOfRooms = p.NoOfRooms,
                    NoOfFloors = p.NoOfFloors,
                    HotelCity = p.HotelCity
                    //Manager = p.AppUser.FirstName + p.AppUser.LastName
                }).ToList();
            }
            return View(listmodel);
        }
    }
}
