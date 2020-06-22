using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewComponents
{
    public class ManagerNabarProfileViewComponent:ViewComponent
    {

        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public ManagerNabarProfileViewComponent(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<ManagerProfileViewModel> GetItemsAsync()
        {
            var model = new ManagerProfileViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {

                model.FirstName = OnlineUser.FirstName;
                model.LastName = OnlineUser.LastName;
                model.Email = OnlineUser.Email;
                model.AvatarImage = OnlineUser.AvatarImage;



            }
            return Task.FromResult(model);
        }
    }
}
