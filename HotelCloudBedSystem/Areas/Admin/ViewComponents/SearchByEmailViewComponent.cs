using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class SearchByEmailViewComponent : ViewComponent
    {
        private IAppUserIEFRepository _repository;
        private UserManager<AppUser> _userManager;

        public SearchByEmailViewComponent(IAppUserIEFRepository repository,UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;


        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<AddUserViewModel> GetItemsAsync()
        {
            AddUserViewModel model = null;
            var result = _userManager.Users;
            if (result != null)
            {
                model = new AddUserViewModel()
                {
                    users = result.Select(p => new SelectListItem()
                    {
                        Text = p.FirstName + p.LastName,
                        Value = p.Id
                    }).ToList(),
                };
            }

            return Task.FromResult(model);
        }
    }
}
