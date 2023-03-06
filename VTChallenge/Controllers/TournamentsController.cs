using Microsoft.AspNetCore.Mvc;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class TournamentsController : Controller {

        public IRepositoryTournaments repo;

        public TournamentsController(IRepositoryTournaments repo) {
            this.repo = repo;
        }

        public IActionResult ListTournaments() {
            List<Tournament> tournaments = this.repo.GetTournaments();
            return View(tournaments);
        }
    }
}
