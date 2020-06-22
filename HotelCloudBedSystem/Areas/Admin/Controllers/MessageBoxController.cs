using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageBoxController : Controller
    {

        [HttpGet]
        public IActionResult Success()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult Failed()
        {
            return PartialView();
        }
    }
}