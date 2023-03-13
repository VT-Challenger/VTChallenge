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
            Users user = HttpContext.Session.GetObject<Users>("USUARIO");
            if (user == null) {
                return RedirectToAction("AccesoDenegado", "Managed");
            } else {
                TempData["LISTTOURNAMENTS"] = this.repo.GetTournaments();
                return View();
            }
        }

        public IActionResult TournamentDetails(int tid) {
            Users user = HttpContext.Session.GetObject<Users>("USUARIO");

            TempData["TOURNAMENT"] = this.repo.GetTournamentComplete(tid);
            TempData["PLAYERSTOURNAMENT"] = this.repo.GetPlayersTournament(tid);
            TempData["ROUNDSNAME"] = this.repo.GetRounds(tid);
            TempData["MATCHESTOURNAMENT"] = this.repo.GetMatchesTournament(tid);
            TempData["TOURNAMENTWINNER"] = this.repo.GetTournamentWinner(tid);
            TempData["VALIDATEINSCRIPTION"] = this.repo.ValidateInscription(tid, user.Uid);
            return View();
        }

        public IActionResult InscriptionPlayer(int tid) {
            Users user = HttpContext.Session.GetObject<Users>("USUARIO");
            this.repo.InscriptionPlayerTeamAle(tid, user.Uid);
        
            return RedirectToAction("TournamentDetails", "Tournaments", new {tid=tid});
        }

        public IActionResult ListTournamentsUser() {
            Users user = HttpContext.Session.GetObject<Users>("USUARIO");
            if (user == null) {
                return RedirectToAction("AccesoDenegado", "Managed");
            } else {
                TempData["LISTTOURNAMENTSUSER"] = this.repo.GetTournamentsUser(user.Name);
                return View();
            }
        }

        public IActionResult DeleteTournament(int tid) {
            this.repo.DeleteTournament(tid);
            return RedirectToAction("ListTournamentsUser", "Tournaments");
        }

        public IActionResult CreateTournament() { return View(); }
    }
}
