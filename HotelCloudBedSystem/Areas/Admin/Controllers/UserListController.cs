using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserListController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IAppUserIEFRepository _repository;

        public UserListController(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IAppUserIEFRepository repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
        }


        #region AllUserList
        [HttpGet]
        public IActionResult AppLicationUser(QueryOptions options,AddUserViewModel model)
        {
            var Userrole = _roleManager.FindByNameAsync("User").Result;

            if(Userrole == null)
            {

            }
            return View(_repository.UsersList(options, Userrole.Id, model.UserId, true));
        }

        //#region Custmer
        //[HttpGet]
        //public IActionResult AppLicationUser()
        //{
        //    int count = 0;
        //    List<UserListViewModel> list = new List<UserListViewModel>();
        //    UserListViewModel model = new UserListViewModel();

        //    var users = _userManager.Users;
        //    var roles = _roleManager.Roles;


        //    foreach (var user in users)
        //    {
        //        var findRoles = _userManager.GetRolesAsync(user).Result;

        //        if (findRoles != null)
        //        {


        //            foreach (var findrole in findRoles)
        //            {

        //                if ((_userManager.IsInRoleAsync(user, findrole).Result) && (findrole == "User"))
        //                {
        //                    count++;
        //                }



        //                if (count > 0)
        //                {
        //                    model = new UserListViewModel()
        //                    {
        //                        AppUserId = user.Id,
        //                        FirstName = user.FirstName,
        //                        LastName = user.LastName,
        //                        Email = user.Email,
        //                        PhoneNo = user.PhoneNumber,
        //                        Created = user.Created,
        //                        role = findrole,
        //                        AccountStatus = user.IsEnable
        //                    };

        //                    list.Add(model);
        //                    count = 0;
        //                }
        //            }
        //        }


        //    }

        //    return View(list);
        //}

        #endregion

        #region NotinRole User

        [HttpGet]
        public IActionResult Manager(QueryOptions options,AddUserViewModel model)
        {

            var mangerUser = _roleManager.FindByNameAsync("Manager").Result;
            var adminnUser= _roleManager.FindByNameAsync("Manager").Result;
            if (mangerUser == null && adminnUser ==null)
            {

            }
            return View(_repository.ManagerialList(options, mangerUser.Id,adminnUser.Id, model.UserId, true));
        }
        //[HttpGet]
        //public IActionResult NotinRole()
        //{
        //    bool isInrole = false;
        //    int count = 0;
        //    List<UserListViewModel> list = new List<UserListViewModel>();
        //    UserListViewModel model = new UserListViewModel();

        //    var users = _userManager.Users;
        //    var roles = _roleManager.Roles;

        //    foreach (var user in users)
        //    {
        //        foreach (var role in roles)
        //        {
        //            isInrole = _userManager.IsInRoleAsync(user, role.Name).Result;

        //            if (isInrole == true)
        //            {
        //                count++;
        //            }

        //        }

        //        if (count == 0)
        //        {
        //            model = new UserListViewModel()
        //            {
        //                AppUserId = user.Id,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                Email = user.Email,
        //                PhoneNo = user.PhoneNumber,
        //                Created = user.Created,
        //                AccountStatus = user.IsEnable
        //            };

        //            list.Add(model);
        //        }
        //        count = 0;
        //    }

        //    return View(list);
        //}



        #endregion

        #region Manager


        [HttpGet]
        public IActionResult NotinRole(QueryOptions options)
        {
            
            return View(_repository.NotinRoleUser(options, true));
        }
        #endregion
        //[HttpGet]
        //public IActionResult Manager()
        //{
        //    int count = 0;
        //    List<UserListViewModel> list = new List<UserListViewModel>();
        //    UserListViewModel model = new UserListViewModel();

        //    var users = _userManager.Users;
        //    var roles = _roleManager.Roles;


        //    foreach (var user in users)
        //    {
        //        var findRoles = _userManager.GetRolesAsync(user).Result;

        //        if (findRoles != null)
        //        {


        //            foreach (var findrole in findRoles)
        //            {

        //                if ((_userManager.IsInRoleAsync(user, findrole).Result) &&
        //                    (findrole == "Manager") || (findrole == "Admin"))
        //                {
        //                    count++;
        //                }

        //                if (count > 0)
        //                {
        //                    model = new UserListViewModel()
        //                    {
        //                        AppUserId = user.Id,
        //                        FirstName = user.FirstName,
        //                        LastName = user.LastName,
        //                        Email = user.Email,
        //                        PhoneNo = user.PhoneNumber,
        //                        Created = user.Created,
        //                        role = findrole,
        //                        AccountStatus = user.IsEnable
        //                    };

        //                    list.Add(model);
        //                    count = 0;
        //                }
        //            }
        //        }


        //    }

        //    return View(list);
        //}

     


        //#region Admin
        //[HttpGet]
        //public IActionResult Admin()
        //{
        //    int count = 0;
        //    List<UserListViewModel> list = new List<UserListViewModel>();
        //    UserListViewModel model = new UserListViewModel();

        //    var users = _userManager.Users;
        //    var roles = _roleManager.Roles;


        //    foreach (var user in users)
        //    {
        //        var findRoles = _userManager.GetRolesAsync(user).Result;

        //        if (findRoles != null)
        //        {


        //            foreach (var findrole in findRoles)
        //            {

        //                if ((_userManager.IsInRoleAsync(user, findrole).Result) && (findrole == "Admin"))
        //                {
        //                    count++;
        //                }



        //                if (count > 0)
        //                {
        //                    model = new UserListViewModel()
        //                    {
        //                        AppUserId = user.Id,
        //                        FirstName = user.FirstName,
        //                        LastName = user.LastName,
        //                        Email = user.Email,
        //                        PhoneNo = user.PhoneNumber,
        //                        Created = user.Created,
        //                        role = findrole
        //                    };

        //                    list.Add(model);
        //                    count = 0;
        //                }
        //            }
        //        }


        //    }

        //    return View(list);
        //}

        //#endregion




    }
}
