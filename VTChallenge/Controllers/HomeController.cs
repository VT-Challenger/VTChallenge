using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VTChallenge.Extensions;
using VTChallenge.Models;
using VTChallenge.Repositories;
using VTChallenge.Services;

namespace VTChallenge.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            if(HttpContext.Session.GetObject<Users>("USUARIO") == null) {
                return RedirectToAction("AccesoDenegado", "Managed");
            } else {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}