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
        private HelperFiles helperFiles;

        public TournamentsController(IRepositoryVtChallenge repo, HelperMails helperMails, HelperFiles helperFiles) {
            this.repo = repo;
            this.helperMails = helperMails;
            this.helperFiles = helperFiles;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> ListTournaments() {
            string rank = HttpContext.User.FindFirst("RANGO").Value;

            ViewData["LISTTOURNAMENTS"] = await this.repo.GetTournamentsByRankAsync(rank.Substring(0, rank.Length - 2).Trim());
            return View();
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> ListTournaments(string filtro) {
            string rank = HttpContext.User.FindFirst("RANGO").Value;

            if(filtro == null) {
                ViewData["LISTTOURNAMENTS"] = await this.repo.GetTournamentsByRankAsync(rank);
            } else {
                ViewData["LISTTOURNAMENTS"] = await this.repo.GetTournamentCompletesFindAsync(filtro, rank.Substring(0, rank.Length - 2).Trim());
            }
            return View();
        }

        [AuthorizeUsers]
        public async Task<IActionResult> TournamentDetails(int tid) {

            ViewData["TOURNAMENT"] = await this.repo.GetTournamentComplete(tid);
            ViewData["PLAYERSTOURNAMENT"] = await this.repo.GetPlayersTournament(tid);
            ViewData["ROUNDSNAME"] = await this.repo.GetRounds(tid);
            ViewData["MATCHESTOURNAMENT"] = await this.repo.GetMatchesTournament(tid);
            ViewData["TOURNAMENTWINNER"] = await this.repo.GetTournamentWinner(tid);
            ViewData["VALIDATEINSCRIPTION"] = await this.repo.ValidateInscription(tid, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }

        [AuthorizeUsers]
        public async Task<IActionResult> InscriptionPlayer(int tid) {
            this.repo.InscriptionPlayerTeamAle(tid, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //DATA CORREOS
            TournamentComplete tournament = await this.repo.GetTournamentComplete(tid);
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
        public async Task<IActionResult> ListTournamentsUser() {
            ViewData["LISTTOURNAMENTSUSER"] = await this.repo.GetTournamentsUser(HttpContext.User.Identity.Name);
            return View();
        }

        [AuthorizeUsers]
        [HttpPost]
        public async  Task<IActionResult> ListTournamentsUser(string filtro) {
            if(filtro == null) {
                ViewData["LISTTOURNAMENTSUSER"] = await this.repo.GetTournamentsUser(HttpContext.User.Identity.Name);
            } else {
                ViewData["LISTTOURNAMENTSUSER"] = await this.repo.GetTournamentsUserFindAsync(HttpContext.User.Identity.Name, filtro);
            }
  
            return View();
        }

        [AuthorizeUsers]
        public async Task<IActionResult> DeleteTournament(int tid) {
            await this.repo.DeleteTournament(tid);
            return RedirectToAction("ListTournamentsUser", "Tournaments");
        }

        [AuthorizeUsers]
        public IActionResult CreateTournament() { return View(); }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> CreateTournament(string jsonTournament, string jsonRounds, string jsonMatches, IFormFile imageTournament) {
            int tid = this.repo.GetMaxIdTournament();
            Tournament tournament = JsonConvert.DeserializeObject<Tournament>(jsonTournament);
            
            string nameImage = "vtchallenge" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value + "-" + tournament.Name.ToString().Replace(" ", "-") + ".jpg";

            string protocol = HttpContext.Request.IsHttps ? "https://" : "http://";
            string domainName = HttpContext.Request.Host.Value.ToString();
            string url = protocol + domainName;
            await this.helperFiles.UploadFileAsync(imageTournament, nameImage , url, Folders.Tournaments);

            await this.repo.InsertTournamentAsync(
                tournament.Tid = tid + 1,
                tournament.Name,
                tournament.Rank,
                tournament.DateInit,
                tournament.Description,
                tournament.Platform,
                tournament.Players,
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                nameImage
            );

            List<Round> rounds = JsonConvert.DeserializeObject<List<Round>>(jsonRounds);
            foreach (Round round in rounds) {
                await this.repo.InsertRoundAsync(
                    round.Name,
                    round.Date,
                    tid + 1
                );
            }

            int roundMatch = this.repo.GetMinIdRoundTournament(tid + 1);
            Round r = await this.repo.FindRoundAsync(roundMatch);
            List<Match> matches = JsonConvert.DeserializeObject<List<Match>>(jsonMatches);
            foreach (Match match in matches) {
                await this.repo.InsertMatchAsync(
                    match.Tblue,
                    match.Tred,
                    r.Date,
                    roundMatch
                );
            }

            return RedirectToAction("ListTournamentsUser", "Tournaments");
        }

        [AuthorizeUsers]
        public async Task<IActionResult> EditTournament(int tid) {
            TournamentComplete tournamentComplete = await this.repo.GetTournamentComplete(tid);
            ViewData["PLAYERSTOURNAMENT"] = await this.repo.GetPlayersTournament(tid);
            ViewData["ROUNDSNAME"] = await this.repo.GetRounds(tid);
            ViewData["MATCHESTOURNAMENT"] = await this.repo.GetMatchesTournament(tid);
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
 
            if(this.repo.TotalMatchesRoundWinner(rid) == this.repo.TotalMatchesRound(rid) && await this.repo.TotalMatchesRound(rid) != 0) {
                await this.repo.InsertMatchesNextRoundAsync(rid);
            }
            return RedirectToAction("EditTournament", "Tournaments", new { tid = tid });
        }
    }
}
