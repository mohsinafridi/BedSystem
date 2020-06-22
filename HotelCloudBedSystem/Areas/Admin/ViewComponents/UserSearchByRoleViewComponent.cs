using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class UserSearchByRoleViewComponent : ViewComponent
    {
        private IAppUserIEFRepository _repository;
        private RoleManager<AppRole> _roleManager;

        public UserSearchByRoleViewComponent(IAppUserIEFRepository repository, RoleManager<AppRole> roleManager)
        {
            _repository = repository;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<AddUserViewModel> GetItemsAsync()
        {
            AddUserViewModel model = null;
            var result = _roleManager.Roles;
            if (result != null)
            {
                model = new AddUserViewModel()
                {
                    Roles = result.Select(p => new SelectListItem()
                    {
                        Text = p.Name,
                        Value = p.Id
                    }).ToList(),
                };
            }

            return Task.FromResult(model);
        }
    }
}
