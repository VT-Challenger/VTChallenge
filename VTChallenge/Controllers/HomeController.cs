using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using VTChallenge.Models;
using VTChallenge.Repositories;
using VTChallenge.Services;

namespace VTChallenge.Controllers {
    public class HomeController : Controller {

        IServiceValorant api;
        IRepositoryUsers repo;

        public HomeController(IServiceValorant api, IRepositoryUsers repo) {
            this.api = api;
            this.repo = repo;
        }

        public IActionResult Index() {
            List<Users> users = this.repo.getUser();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}