using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class PieChartViewComponent:ViewComponent
    {
        private IAppUserIEFRepository _repository;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;

        public PieChartViewComponent(IAppUserIEFRepository repository,
            RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            _repository = repository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            //List<UserListViewModel> listmodel = new List<UserListViewModel>();


            //var ManagerUsers = _userManager.GetUsersInRoleAsync("Manager").Result;

            //if (ManagerUsers != null)
            //{
            //    listmodel = ManagerUsers.Select(p => new UserListViewModel()
            //    {
            //        FirstName = p.FirstName,
            //        LastName = p.LastName,
            //        Email = p.Email,
            //        Isenbl = p.IsEnable,
            //        role=p.AppRole.Name
            //    }).ToList();
            //}
            //return View(listmodel);
            return View();
        }

        //private Task<UserListViewModel> GetItemsAsync(QueryOptions options)
        //{
        //    List<UserListViewModel> listmodel = new List<UserListViewModel>();

            
        //    var ManagerUsers = _userManager.GetUsersInRoleAsync("Manager").Result;

        //    if (ManagerUsers != null)
        //    {
        //        listmodel = ManagerUsers.Select(p => new UserListViewModel()
        //        {
        //            FirstName = p.FirstName,
        //        LastName = p.LastName,
        //        Email = p.Email,
        //        Isenbl = p.IsEnable,
        //    }).ToList();
        //    }
           
            
        //    //return View(_repository.ManagerialList(options, mangerUser.Id, adminnUser.Id,null, true));
        //    //var count = new DashBoardCountViewModel();
        //    //count.TotalUserCount = _repository.ToTalUserCount();
        //    //count.RoleCount = _repository.RolesCount();
        //    //count.ManagerCount = _repository.ManagerCount();
        //    //count.AdminCount = _repository.AdminCount();
        //    //count.EndUserCount = _repository.EnDUserCount();

        //    return Task.FromResult(listmodel);

        //}
    }
}
