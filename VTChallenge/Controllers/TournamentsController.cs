using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VTChallenge.Extensions;
using VTChallenge.Filters;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class TournamentsController : Controller {

        public IRepositoryVtChallenge repo;

        public TournamentsController(IRepositoryVtChallenge repo) {
            this.repo = repo;
        }

        [AuthorizeUsers]
        public IActionResult ListTournaments() {
            ViewData["LISTTOURNAMENTS"] = this.repo.GetTournaments();
            return View();
        }

        [AuthorizeUsers]
        public IActionResult TournamentDetails(int tid) {

            ViewData["TOURNAMENT"] = this.repo.GetTournamentComplete(tid);
            ViewData["PLAYERSTOURNAMENT"] = this.repo.GetPlayersTournament(tid);
            ViewData["ROUNDSNAME"] = this.repo.GetRounds(tid);
            ViewData["MATCHESTOURNAMENT"] = this.repo.GetMatchesTournament(tid);
            ViewData["TOURNAMENTWINNER"] = this.repo.GetTournamentWinner(tid);
            ViewData["VALIDATEINSCRIPTION"] = this.repo.ValidateInscription(tid, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }

        public IActionResult InscriptionPlayer(int tid) {
            this.repo.InscriptionPlayerTeamAle(tid, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
            return RedirectToAction("TournamentDetails", "Tournaments", new {tid=tid});
        }

        [AuthorizeUsers]
        public IActionResult ListTournamentsUser() {
            ViewData["LISTTOURNAMENTSUSER"] = this.repo.GetTournamentsUser(HttpContext.User.Identity.Name);
            return View();
        }

        public IActionResult DeleteTournament(int tid) {
            this.repo.DeleteTournament(tid);
            return RedirectToAction("ListTournamentsUser", "Tournaments");
        }

        [AuthorizeUsers]
        public IActionResult CreateTournament() { return View(); }
    }
}
