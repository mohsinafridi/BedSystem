using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class DashBoardCountViewComponent : ViewComponent
    {
        private IAppUserIEFRepository _repository;
        private UserManager<AppUser> _userManager;

        public DashBoardCountViewComponent(IAppUserIEFRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<DashBoardCountViewModel> GetItemsAsync()
        {
            var count = new DashBoardCountViewModel();
            int Admincount = 0, ManagerCount = 0, NotinRoleCount = 0, EndUserCount = 0;

            count.TotalUserCount = _repository.ToTalUserCount();
            count.RoleCount = _repository.RolesCount();
            count.EnabledUserCount = _repository.EnabledUsers();
            count.DisabledUserCount = _repository.DisabledUsers();
            count.HotelCount = _repository.HotelCount();

            var users = _userManager.Users;

            foreach (var user in users)
            {
                if (_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    Admincount++;
                }
                if (_userManager.IsInRoleAsync(user, "Manager").Result)
                {
                    ManagerCount++;
                }
                if (_userManager.IsInRoleAsync(user, "User").Result)
                {
                    EndUserCount++;
                }

                if (!(_userManager.IsInRoleAsync(user, "Admin").Result ||
                     _userManager.IsInRoleAsync(user, "Manager").Result ||
                     _userManager.IsInRoleAsync(user, "User").Result))
                {
                    NotinRoleCount++;
                }



            }

            count.InRole = Admincount + ManagerCount + EndUserCount;
            count.AdminCount = Admincount;
            count.ManagerCount = ManagerCount;
            count.EndUserCount = EndUserCount;
            count.NotInRole = NotinRoleCount;




            return Task.FromResult(count);

        }
    }
}
