using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;

namespace HotelCloudBedSystem.Areas.Admin.ViewComponents
{
    public class SalesChartViewComponent:ViewComponent
    {
        private IAppUserIEFRepository _repository;

        public SalesChartViewComponent(IAppUserIEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<DashBoardCountViewModel> GetItemsAsync()
        {

            var count = new DashBoardCountViewModel();
            count.TotalUserCount = _repository.ToTalUserCount();
            count.RoleCount = _repository.RolesCount();
            count.ManagerCount = _repository.ManagerCount();
            count.AdminCount = _repository.AdminCount();
            count.EndUserCount = _repository.EnDUserCount();


            return Task.FromResult(count);

        }
    }
}
