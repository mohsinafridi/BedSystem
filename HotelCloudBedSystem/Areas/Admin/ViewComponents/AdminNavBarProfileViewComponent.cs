using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class AdminNavBarProfileViewComponent:ViewComponent
    {

        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public AdminNavBarProfileViewComponent(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<AdminProfileViewModel> GetItemsAsync()
        {
            var model = new AdminProfileViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (OnlineUser != null  && _userManager.IsInRoleAsync(OnlineUser, "Admin").Result)
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
