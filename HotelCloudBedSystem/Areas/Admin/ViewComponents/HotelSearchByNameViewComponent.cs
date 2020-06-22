using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class HotelSearchByNameViewComponent:ViewComponent
    {
        private IEFRepository _repository;

        public HotelSearchByNameViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<HotelSearchedByNameViewModel> GetItemsAsync()
        {
            HotelSearchedByNameViewModel model = null;
            var result = _repository.GetHotels();
            if (result != null)
            {
                model = new HotelSearchedByNameViewModel()
                {
                    Hotel = result.Select(p => new SelectListItem()
                    {
                        Text = p.HotelName,
                        Value = p.HotelId.ToString()
                    }).ToList(),
                };
            }

            return Task.FromResult(model);
        }
    }
}
