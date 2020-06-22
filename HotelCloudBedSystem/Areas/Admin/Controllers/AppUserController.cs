using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AppUserController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IEmailSender _emailSender;
        private IAppUserIEFRepository _repository;

        public AppUserController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
           IEmailSender emailSender,
           IAppUserIEFRepository repository
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _repository = repository;
        }


        #region AllUserList
        [HttpGet]
        public async Task<IActionResult> AllUserList(QueryOptions options)
        {
            return View(await _repository.AllUsersList(options,null,true));
        }


        //public IActionResult AllUserList()

        //{
        //    List<UserListViewModel> userlist = new List<UserListViewModel>();
        //    UserListViewModel model = new UserListViewModel();
        //    var users = _userManager.Users;
        //    foreach (var user in users)
        //    {
        //        var findroles = _userManager.GetRolesAsync(user).Result;
        //        if (findroles.Count > 0)
        //        {
        //            foreach (var role in findroles)
        //            {
        //                model = new UserListViewModel()
        //                {
        //                    AppUserId = user.Id,
        //                    FirstName = user.FirstName,
        //                    LastName = user.LastName,
        //                    Email = user.Email,
        //                    PhoneNo = user.PhoneNumber,
        //                    Created = user.Created,
        //                    role = role,
        //                    AccountStatus = user.IsEnable
        //                };
        //                userlist.Add(model);



        //            }
        //        }
        //        if (findroles.Count == 0)
        //        {
        //            model = new UserListViewModel()
        //            {
        //                AppUserId = user.Id,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                Email = user.Email,
        //                PhoneNo = user.PhoneNumber,
        //                Created = user.Created,
        //                role = "Not in role"
        //            };

        //            userlist.Add(model);
        //        }


        //    }
        //    return View(userlist);
        //}

        #endregion


        #region AddUser
        [HttpGet]
        public IActionResult AddAppUser()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAppUser(AddUserViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            var Usermodel = new UserListViewModel();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "invalid or incomplete information");
            }

            if (model == null)
            {
                return NotFound("Empty data found");
            }
            else
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Created = DateTime.Now,
                    UserName = model.Email,
                    Email = model.Email,
                    IsEnable = true
                };


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Status = true;
                    Message = $"User :{model.FirstName+model.LastName} : Added Successfullllllly";
                   
                }
                else
                {
                    Status = true;
                    Message = $"Error Adding User :{model.FirstName+model.LastName} :";
                }
            }
           

            return Json(new { status = Status, message = Message });
        }


        #endregion

        #region DeleterUser

        [HttpGet]
        public IActionResult DeleteView(string id)
        {
            List<UserDeleteViewModel> listmodel = new List<UserDeleteViewModel>();

            if(id == null)
            {

            }
            var user = _userManager.FindByIdAsync(id).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

           
            if (user ==null)
            {

            }

           var usermodel= new UserDeleteViewModel()
            {
               FirstName=user.FirstName,
               LastName=user.LastName,
               Email=user.Email,
               PhoneNo=user.PhoneNumber,
               UserId=user.Id
            };

            foreach (var role in roles)
            {
                usermodel.RolesList.Add(role);
            }
            return View(usermodel);
        }
        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManager.FindByIdAsync(id).Result;
            var roles = _roleManager.Roles;
            int count = 0;

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    if (role.Name != "User")
                    {
                        var userrole = _userManager.IsInRoleAsync(user, role.Name).Result;
                        if (userrole)
                        {
                            count++;
                        }

                    }

                }
            }

            if (user == null)
            {
                NotFound("user not found");
            }

            if (count == 0)
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    Status = true;
                    Message = $"User  :{user.FirstName+user.LastName} :  deleted successfuly";
                   
                }
                else
                {
                    Status = false;
                    Message = $"Error in deleting User :{user.FirstName+user.LastName} :";
                   
                }
            }
            else if (count > 0)
            {
                Status = false;
                Message = $"User :  {user.FirstName+user.LastName} :  is In Managerial Role....first Remove role to delete user";
               
            }






            return Json(new { status = Status, message = Message });
        }


        #endregion

        #region UpdateUser

        [HttpGet]
        public IActionResult _UpdateUser(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            var model = new UpdateUserViewModel()
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Created = user.Created,
                UserName = user.Email,
                Email = user.Email
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _UpdateUser(UpdateUserViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManager.FindByEmailAsync(model.Email).Result;

            if(user == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "invalid or incomplete information");
            }

            if (model == null)
            {
                return NotFound("Empty data found");
            }
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Created = DateTime.Now;
                user.UserName = model.Email;
                user.Email = model.Email;
                user.PhoneNumber = "03484686261";
                user.IsEnable = true;
                


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    Status = true;
                    Message = $"User:{user.FirstName+user.LastName}: Updated Successfulllllly";
                  
                }
                else
                {
                    Status = true;
                    Message = $"Error Occured Updating User :{user.FirstName+user.LastName}:";
                }
            }


            return Json(new { status = Status, message = Message });
        }

        #endregion
    }
}
