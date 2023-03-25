using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using VTChallenge.Extensions;
using VTChallenge.Models;
using VTChallenge.Models.Api;
using VTChallenge.Repositories;
using VTChallenge.Services;

namespace VTChallenge.Controllers {
    public class ManagedController : Controller {

        IRepositoryVtChallenge repo;
        IServiceValorant api;

        public ManagedController(IRepositoryVtChallenge repo, IServiceValorant api) {
            this.repo = repo;
            this.api = api;
        }

        public IActionResult Login() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password) {
            username = "Popolas";
            password = "P@ssw0rd";
            Users user = await this.repo.LoginNamePasswordAsync(username, password);
            if (user != null) {
                ClaimsIdentity identity = new ClaimsIdentity(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   ClaimTypes.Name,
                   ClaimTypes.Role
                );

                Claim claimName = new Claim(ClaimTypes.Name, username);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.Uid.ToString());
                Claim claimRole = new Claim(ClaimTypes.Role, user.Rol);
                Claim claimRank = new Claim("RANGO", user.Rank);
                Claim claimTag = new Claim("TAG", user.Tag);
                Claim claimEmail = new Claim("EMAIL", user.Email);
                Claim claimImageLarge = new Claim("IMAGELARGE", user.ImageLarge);
                Claim claimImageSmall = new Claim("IMAGESMALL", user.ImageSmall);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimRank);
                identity.AddClaim(claimTag);
                identity.AddClaim(claimEmail);
                identity.AddClaim(claimImageLarge);
                identity.AddClaim(claimImageSmall);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

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
                user.Rank = await this.api.GetRankAsync(user.Name, user.Tag);
            }
            if (data != null) {
                await this.repo.RegisterUserAsync(user.Uid, user.Name, user.Tag, user.Email, user.Password, user.ImageSmall, user.ImageLarge, user.Rank);
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Landing");
        }
    }
}
