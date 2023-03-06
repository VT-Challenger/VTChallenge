using Microsoft.AspNetCore.Mvc;
using VTChallenge.Extensions;
using VTChallenge.Models;
using VTChallenge.Repositories;
using VTChallenge.Services;

namespace VTChallenge.Controllers {
    public class ManagedController : Controller {

        IRepositoryUsers repo;
        IServiceValorant api;

        public ManagedController(IRepositoryUsers repo, IServiceValorant api) {
            this.repo = repo;
            this.api = api;
        }

        public IActionResult Login() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(string username, string password) {
            username= "Popolas";
            password = "1234";
            Users user = this.repo.LoginNamePassword(username, password);
            if(user != null) {
                //ALMACENAR USUARIO EN SESION
                HttpContext.Session.SetObject("USUARIO", user);
                return RedirectToAction("Index", "Home");
            } else {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public IActionResult Register() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(Users user) {
            DataApi data = null;
            UserApi userapi = await this.api.GetAccountAsync(user.Name, user.Tag);
            if (userapi != null) {
                data = userapi.Data;

                user.Uid = data.Puuid;
                user.ImageLarge = data.Card.Large;
                user.ImageSmall = data.Card.Small;
            }
            if (data != null) {
                await this.repo.RegisterUserAsync(user.Uid, user.Name, user.Tag, user.Email, user.Password, user.ImageLarge, user.ImageLarge, user.Rank);
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult AccesoDenegado() {
            return View();
        }
    }
}
