using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VTChallenge.Extensions;
using VTChallenge.Filters;
using VTChallenge.Helpers;
using VTChallenge.Models;
using VTChallenge.Repositories;

namespace VTChallenge.Controllers {
    public class TournamentsController : Controller {

        private IRepositoryVtChallenge repo;
        private HelperMails helperMails;

        public TournamentsController(IRepositoryVtChallenge repo, HelperMails helperMails) {
            this.repo = repo;
            this.helperMails = helperMails;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> ListTournaments() {
            string rank = "Diamond";
            List<TournamentComplete> tournamentsUser = await this.repo.GetTournamentsByRankAsync(rank);

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

        public async Task<IActionResult> InscriptionPlayer(int tid) {
            this.repo.InscriptionPlayerTeamAle(tid, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //DATA CORREOS
            TournamentComplete tournament = this.repo.GetTournamentComplete(tid);
            string user = HttpContext.User.Identity.Name;
            int espacios = tournament.LimitPlayers - tournament.Inscriptions;
            string contenidoOrg = this.helperMails.PlantillaInscriptionOrg(user, espacios);
            string contenidoPlayer = this.helperMails.PlantillaInscriptionPlayer(user, tournament.Name, tournament.DateInit.ToString(), tournament.Platform, tournament.Organizator);

            //CORREO AL USUARIO
            await this.helperMails.SendMailAsync(HttpContext.User.FindFirst("EMAIL").Value.ToString(), "INSCRIPCION", contenidoPlayer);

            //CORREO AL ORGANIZADOR (PONER CORREO DE ORGANIZADOR)
            await this.helperMails.SendMailAsync(HttpContext.User.FindFirst("EMAIL").Value.ToString(), "INSCRIPCION", contenidoOrg);

            return RedirectToAction("TournamentDetails", "Tournaments", new { tid = tid });
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
