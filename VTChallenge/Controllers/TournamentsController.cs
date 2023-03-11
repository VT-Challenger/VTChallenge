using Microsoft.AspNetCore.Mvc;
using VTChallenge.Extensions;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class TournamentsController : Controller {

        public IRepositoryVtChallenge repo;

        public TournamentsController(IRepositoryVtChallenge repo) {
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
            TempData["ROUNDSNAME"] = this.repo.GetRounds(tid);
            TempData["MATCHESTOURNAMENT"] = this.repo.GetMatchesTournament(tid);
            TempData["TOURNAMENTWINNER"] = this.repo.GetTournamentWinner(tid);
            return View(tournament);
        }

        public IActionResult InscriptionPlayer(int tid) {
            Users user = HttpContext.Session.GetObject<Users>("USUARIO");
            this.repo.InscriptionPlayerTeamAleASync(tid, user.Uid);
            return RedirectToAction("Index", "Home");
        }
    }
}
