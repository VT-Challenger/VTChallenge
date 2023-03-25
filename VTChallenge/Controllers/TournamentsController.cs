using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private HelperJson helperJson;

        public TournamentsController(IRepositoryVtChallenge repo, HelperMails helperMails, HelperJson helperJson) {
            this.repo = repo;
            this.helperMails = helperMails;
            this.helperJson = helperJson;
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

        [AuthorizeUsers]
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
            Users organizador = await this.repo.FindUserByNameAsync(tournament.Organizator);
            await this.helperMails.SendMailAsync(organizador.Email, "INSCRIPCION", contenidoOrg);

            return RedirectToAction("TournamentDetails", "Tournaments", new { tid = tid });
        }

        [AuthorizeUsers]
        public IActionResult ListTournamentsUser() {
            ViewData["LISTTOURNAMENTSUSER"] = this.repo.GetTournamentsUser(HttpContext.User.Identity.Name);
            return View();
        }

        [AuthorizeUsers]
        public IActionResult DeleteTournament(int tid) {
            this.repo.DeleteTournament(tid);
            return RedirectToAction("ListTournamentsUser", "Tournaments");
        }

        [AuthorizeUsers]
        public IActionResult CreateTournament() { return View(); }

        [AuthorizeUsers]
        public IActionResult EditTournament(int tid) {
            TournamentComplete tournamentComplete = this.repo.GetTournamentComplete(tid);
            ViewData["PLAYERSTOURNAMENT"] = this.repo.GetPlayersTournament(tid);
            ViewData["ROUNDSNAME"] = this.repo.GetRounds(tid);
            ViewData["MATCHESTOURNAMENT"] = this.repo.GetMatchesTournament(tid);
            return View(tournamentComplete);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> DeleteUserTournament(int tid, string uid) {
            await this.repo.DeteleUserTournamentAsync(tid, uid);
            Users player = await this.repo.FindUserAsync(uid);
            string contenido = this.helperMails.PlantillaRemoveUserTournament();
            await this.helperMails.SendMailAsync(player.Email, "HAS SIDO EXPULSADO DEL TORNEO", contenido);
            return RedirectToAction("EditTournament", "Tournaments", new { tid = tid });
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> UpdateUserTournament(int tid, string data) {
            List<Match> partidas = JsonConvert.DeserializeObject<List<Match>>(data);
            int rid = partidas[partidas.Count -1].Rid;
            foreach (Match match in partidas) {
                await this.repo.UpdateMatchesTournamentAsync(match.Mid, match.Rblue, match.Rred);
            }
 
            if(this.repo.TotalMatchesRoundWinner(rid) == this.repo.TotalMatchesRound(rid) && this.repo.TotalMatchesRound(rid) != 0) {
                await this.repo.InsertMatchesNextRoundAsync(rid);
            }
            return RedirectToAction("EditTournament", "Tournaments", new { tid = tid });
        }
    }
}
