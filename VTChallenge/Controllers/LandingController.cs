using Microsoft.AspNetCore.Mvc;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class LandingController : Controller {

        private IRepositoryVtChallenge repo;

        public LandingController(IRepositoryVtChallenge repo) {
            this.repo = repo;
        }

        public async Task<IActionResult> Index() {
            List<TournamentComplete> tournaments = await this.repo.GetTournaments();
            return View(tournaments);
        }
    }
}
