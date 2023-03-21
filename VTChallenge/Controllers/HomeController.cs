using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VTChallenge.Extensions;
using VTChallenge.Filters;
using VTChallenge.Models;
using VTChallenge.Repositories;
using VTChallenge.Services;

namespace VTChallenge.Controllers {
    public class HomeController : Controller {

        [AuthorizeUsers]
        public IActionResult Index() {
           return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}