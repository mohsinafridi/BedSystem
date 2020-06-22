using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUserRoleController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public AppUserRoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public  IActionResult AssignRole(string id)
        {
            var model = new AssignRoleListViewModel { AppUserId = id };
            AssignRoleListViewModel.StaticUserId = id;
            var user =  _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                NotFound("User Not Found");
            }

            var roles = _roleManager.Roles;

            if (roles != null)
            {
                foreach (var role in roles)
                {

                    var result = _userManager.IsInRoleAsync(user, role.Name).Result;

                    var userrole = new AssignRoleViewModel()
                    {
                        AppRoleId = role.Id.ToString(),
                        Created = role.Created,
                        Name = role.Name,
                    };

                    if (result)
                    {
                        userrole.IsAssigned = true;
                    }

                    model.AssignRole.Add(userrole);
                }

            }
            return PartialView(model);


        }


        [HttpPost]

        public async Task<IActionResult> AssignRole(AssignRoleListViewModel model)
         {

            bool Status = false;
            string roleid=string.Empty;
            string Message = string.Empty;

            var User = await _userManager.FindByIdAsync(AssignRoleListViewModel.StaticUserId).ConfigureAwait(true);
            var roles = _userManager.GetRolesAsync(User).Result;


            if (ModelState.IsValid)
            {
                foreach (var role in model.AssignRole)
                {
                    var userrole = _userManager.IsInRoleAsync(User, role.AppRoleId);

                    if (userrole != null)
                    {
                        if (role.IsAssigned == false)
                        {
                            await _userManager.RemoveFromRoleAsync(User, role.Name).ConfigureAwait(true);
                            Status = true;
                            Message = "Data Remove successfully";
                        }
                    }

                    if (role.IsAssigned)
                    {

                        foreach (var checkrole in roles)
                        {
                            var Findroleasync = _userManager.IsInRoleAsync(User, checkrole).Result;

                        }
                       
                        await _userManager.AddToRoleAsync(User, role.Name).ConfigureAwait(true);

                        var findrole = _roleManager.FindByIdAsync(role.AppRoleId);
                        User.AppRole= await findrole;
                        await _userManager.UpdateAsync(User).ConfigureAwait(true);

                    }
                }
               
            }


            Status = true;
            Message = "Data updated successfully";

            ModelState.AddModelError("", "Provide all required data to proceed");
            return Json(new { status = Status, message = Message });

        }
    }
}
