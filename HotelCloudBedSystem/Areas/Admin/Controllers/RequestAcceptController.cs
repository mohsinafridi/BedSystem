using System.Threading.Tasks;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestAcceptController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEmailSender _emailSender;
        private ISmsSender _msSender;
        private IAppUserIEFRepository _repository;

        public RequestAcceptController(
            UserManager<AppUser> userManager, 
            IEmailSender emailSender,
            ISmsSender msSender,
            IAppUserIEFRepository repository)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _msSender = msSender;
            _repository = repository;
        }
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(true);

            if (user != null)
            {
                user.RequestAccept = true;

            }
            var result = _userManager.UpdateAsync(user).Result;

            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(user.Email, "Account Acceptance Mail",
                      $"Your Accunt Has been Accepted Successfuly...Now you can use Your Buisness Account")
                    .ConfigureAwait(true);
                await _msSender.SendSmsAsync(user.PhoneNumber, "Your Accunt Has been Accepted Successfuly.." +
                    ".Now you can use Your Buisness Account").ConfigureAwait(true);

                return RedirectToAction("Accept", "RequestAccept");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Accept(QueryOptions options)
        {

            return View(await _repository.AcceptedRequest(options, true).ConfigureAwait(true));


        }
    }
}