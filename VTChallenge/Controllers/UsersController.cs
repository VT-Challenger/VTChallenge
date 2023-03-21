using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VTChallenge.Filters;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class UsersController : Controller {

        private IRepositoryVtChallenge repo;

        public UsersController(IRepositoryVtChallenge repo) {
            this.repo = repo;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> ProfileUser() {
            Users user = await this.repo.FindUserAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ViewData["TORNEOSGANADOS"] = this.repo.GetTotalWinsAsync(user.Uid);
            return View(user);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> UpdateProfile(string uid) {
            //HAY QUE ACTUALIZAR LOS CLAIMS TAMBIEN
            await this.repo.UpdateProfileAsync(uid);
            return RedirectToAction("LogOut", "Managed");
        }
    }
}
