using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Controllers
{
    public class NotAcceptedAccountMsgController : Controller
    {
        [HttpGet]
        public IActionResult AcceptMessage()
        {
            return View();
        }
    }
}