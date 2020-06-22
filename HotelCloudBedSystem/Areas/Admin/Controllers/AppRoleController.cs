using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppRoleController : Controller
    {
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IAppUserIEFRepository _repository;

        public AppRoleController(RoleManager<AppRole> roleManager, 
            UserManager<AppUser> userManager, IAppUserIEFRepository repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index(QueryOptions options,string roleId)
        {

            return View(_repository.GetRoles(options, roleId, true));
        }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    List<AppRoleListViewModel> model = new List<AppRoleListViewModel>();

        //    var role = _roleManager.Roles;

        //    model = role.Select(p => new AppRoleListViewModel()
        //    {
        //        AppRoleId=p.Id,
        //        RoleName = p.Name,
        //        Description = p.Description,
        //        Created = p.Created
        //    }).ToList();


        //    return View(model);
        //}


        [HttpGet]
        public IActionResult RoleCreate()
        {
            AppRoleCreateViewModel model = new AppRoleCreateViewModel()
            {
                Created = DateTime.Now
            };
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult RoleCreate(AppRoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    var result = _roleManager.CreateAsync(new AppRole()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Created = DateTime.Now

                    }).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { area = "Admin", controller = "AppRole" });

                    }

                }
            }

            ModelState.AddModelError("", "Required info missing");
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult RoleUpdate(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;

            if(role != null)
            {
                var model = new AppRoleCreateViewModel()
                {
                    RoleId=role.Id,
                    Name=role.Name,
                    Created=role.Created,
                    Description=role.Description
                };
                return PartialView(model);
            }
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleUpdate(AppRoleCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    var result = _roleManager.UpdateAsync(new AppRole()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Created = DateTime.Now

                    }).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { area = "Admin", controller = "AppRole" });

                    }
                }
            }
                    return PartialView();
        }

        [HttpGet]
        public IActionResult DeleteRole(string id)
        {
            bool Status = false;
            string Message = string.Empty;
            var role = _roleManager.FindByIdAsync(id).Result;
            int count = 0;
            var users = _userManager.Users;

            if (role != null)
            {
                if (users != null)
                {

                    foreach (var user in users)
                    {
                        var userrole = _userManager.IsInRoleAsync(user, role.Name).Result;

                        if (userrole)
                        {
                            count++;
                        }
                    }

                }
            }

            if (count == 0)
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    Status = true;
                    Message = "Role deleted successfuly";

                }
                else
                {
                    Status = false;
                    Message = "Error in deleting User";

                }
            }
            else if (count > 0)
            {
                Status = false;
                Message = $"{role.Name} :Role is Assign TO the User ....Cannot Delete this role";

            }






            return Json(new { status = Status, message = Message });
        }


    }
}
