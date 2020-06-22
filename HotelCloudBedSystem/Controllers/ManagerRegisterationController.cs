using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.Services;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Controllers
{
    public class ManagerRegisterationController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IEmailSender _emailSender;
        private ISmsSender _msSender;

        public ManagerRegisterationController(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager ,
             IEmailSender emailSender,
            ISmsSender msSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _msSender = msSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ManagerRegisterationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNo,
                    Aboutyou = model.Aboutyou,
                    Address = model.Address,
                    UserName=model.Email,
                    RequestAccept=false,
                    IsEnable=true

                };

                var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(user.Email, "You have been registered as manager",
                     $"Now Register your Hotel")
                   .ConfigureAwait(true);
                    await _msSender.SendSmsAsync(user.PhoneNumber, "You have been registered as manager" +
                        "Now Register your Hotel").ConfigureAwait(true);
                    var roles = _roleManager.Roles;
                    int count = 0;
                    foreach (var role in roles)
                    {
                        var userrole = _userManager.IsInRoleAsync(user, role.Name).Result;
                        if (userrole == true)
                        {
                            count++;
                        }

                    }
                    if (count == 0)
                    {
                        var result1 = await _userManager.AddToRoleAsync(user, "Manager").ConfigureAwait(true);
                    }
                    return RedirectToAction("HotelRegister", "HotelRegisteration", new { email = model.Email });
                }
            }
                return View();
        }
    }
}