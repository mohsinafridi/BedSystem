using System.Threading.Tasks;
using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AccountRequestController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IAppUserIEFRepository _repository;

        public AccountRequestController(UserManager<AppUser> userManager,IAppUserIEFRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }
        public async Task<IActionResult> Index(QueryOptions options)
        {
            
          return View(await _repository.AccountRequest(options,true).ConfigureAwait(true));

         
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            AdminProfileViewModel model = new AdminProfileViewModel();
            if(id == null)
            {

            }

            var user = _userManager.FindByIdAsync(id).Result;
            if(user == null)
            {

            }
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.PhoneNo = user.PhoneNumber;
            model.EmailConfirmed = user.EmailConfirmed;
            model.IsEnabled = user.IsEnable;
            return View(model);
        }
    }
}