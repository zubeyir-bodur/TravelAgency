using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly TravelAgencyContext dbContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        public IActionResult Index()
        {
            // Dummy method for checking whether connection actually works.
            var user = dbContext.Users.SingleOrDefault();
            var name = user.FirstName;
            ViewData["name"] = name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
