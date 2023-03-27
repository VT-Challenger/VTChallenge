using Microsoft.AspNetCore.Mvc;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class LandingController : Controller {

        private IRepositoryVtChallenge repo;

        public LandingController(IRepositoryVtChallenge repo) {
            this.repo = repo;
        }

        public IActionResult Index() {
            List<TournamentComplete> tournaments = this.repo.GetTournaments();
            return View(tournaments);
        }
    }
}
