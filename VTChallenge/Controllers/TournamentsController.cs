using Microsoft.AspNetCore.Mvc;
using VTChallenge.Extensions;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class TournamentsController : Controller {

        public IRepositoryTournaments repo;

        public TournamentsController(IRepositoryTournaments repo) {
            this.repo = repo;
        }

        public IActionResult ListTournaments() {
            if (HttpContext.Session.GetObject<Users>("USUARIO") == null) {
                return RedirectToAction("AccesoDenegado", "Managed");
            } else {
                List<TournamentComplete> tournaments = this.repo.GetTournaments();
                return View(tournaments);
            }
        }

        public IActionResult TournamentDetails(int tid) {
            TournamentComplete tournament = this.repo.GetTournamentComplete(tid);
            TempData["PLAYERSTOURNAMENT"] = this.repo.GetPlayersTournament(tid);
            return View(tournament);
        }
    }
}
