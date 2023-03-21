using Microsoft.AspNetCore.Mvc;

namespace VTChallenge.Controllers {
    public class LandingController : Controller {

        public IActionResult Index() {
            return View();
        }
    }
}
