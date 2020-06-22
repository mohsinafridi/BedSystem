using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Controllers
{
    public class CityController : Controller
    {
        private IEFRepository _repository;
        private HotelCloudDbContext _context;

        public CityController(IEFRepository repository,HotelCloudDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Autocomplete(string term)
        {
            var result=_context.cities.
#pragma warning disable CA1307 // Specify StringComparison
                Where(p => p.CityName.Contains(term)).
#pragma warning restore CA1307 // Specify StringComparison
                Select(p => p.CityName).ToList();

            return Json(result.ToArray());
        }

        
    }
    
}