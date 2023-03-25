using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using VTChallenge.Data;
using VTChallenge.Helpers;
using VTChallenge.Models;
using VTChallenge.Models.Api;
using VTChallenge.Services;

namespace VTChallenge.Repositories {

    #region PROCEDURES

    #endregion

    public class RepositoryVtChallenge : IRepositoryVtChallenge {

        private VTChallengeContext context;
        private IServiceValorant api;

        public RepositoryVtChallenge(VTChallengeContext context, IServiceValorant api) {
            this.context = context;
            this.api = api;
        }

        #region METHODS USERS
        public List<Users> getUser() {
            var consulta = from data in this.context.Users
                           select data;
            return consulta.ToList();
        }

        public async Task<Users> FindUserAsync(string uid) {
            return await this.context.Users.FirstOrDefaultAsync(x => x.Uid == uid);
        }

        public async Task<Users> FindUserByNameAsync(string name) {
            return await this.context.Users.FirstOrDefaultAsync(x => x.Name == name);
        }


        public async Task<Users> LoginNamePasswordAsync(string username, string password) {
            Users user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null) {
                return null;
            } else {
                byte[] passUsuario = user.PassEncript;
                string salt = user.Salt;

                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);

                if (respuesta == true) {
                    return user;
                } else {
                    return null;
                }
            }
        }

        public async Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string imagelarge, string rango) {
            Users user = new Users();

            user.Uid = uid;
            user.Name = name;
            user.Tag = tag;
            user.Email = email;
            user.Password = password;
            user.ImageSmall = imagesmall;
            user.ImageLarge = imagelarge;
            user.Rank = rango;
            user.Rol = "player";
            user.Salt = HelperCryptography.GenerateSalt();
            user.PassEncript = HelperCryptography.EncryptPassword(password, user.Salt);

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> GetTotalWinsAsync(string uid) {
            var totalWins = (from m in this.context.Matches
                             join r in this.context.Rounds on m.Rid equals r.Rid
                             join tp in this.context.TournamentPlayers on r.Tid equals tp.Tid
                             where r.Name == "Final" &&
                             ((m.Tred == tp.Tid && m.Rred > m.Rblue) || (m.Tblue == tp.Tid && m.Rblue > m.Rred)) &&
                             tp.Uid == uid
                             select m).Count();
            return (int)totalWins;
        }

        public async Task UpdateProfileAsync(string uid) {
            DataApi data = null;
            UserApi userapi = await this.api.GetAccountUidAsync(uid);
            Users user = await this.FindUserAsync(uid);

            if (userapi != null) {
                data = userapi.Data;
                user.ImageLarge = data.Card.Large;
                user.ImageSmall = data.Card.Small;
                user.Rank = await this.api.GetRankAsync(user.Name, user.Tag);
            }
            await this.context.SaveChangesAsync();
        }
        #endregion

        #region METHODS TOURNAMENTS
        public List<TournamentPlayers> GetPlayersTournament(int tid) {
            var consulta = from data in this.context.TournamentPlayers
                           where data.Tid == tid
                           select data;
            return consulta.OrderBy(x => x.Team).ToList();
        }

        public TournamentComplete GetTournamentComplete(int tid) {
            return this.context.TournamentCompletes.FirstOrDefault(z => z.Tid == tid);
        }

        public List<TournamentComplete> GetTournaments() {
            var consulta = from data in this.context.TournamentCompletes
                           select data;

            return consulta.ToList();
        }

        public async Task<List<TournamentComplete>> GetTournamentsByRankAsync(string rank) {
            var consulta = from data in this.context.TournamentCompletes
                           where data.Rank.Contains(rank)
                           select data;

            return await consulta.ToListAsync();
        }

        public List<Round> GetRounds(int tid) {
            var consulta = from data in this.context.Rounds
                           where data.Tid == tid
                           select data;
            return consulta.ToList();
        }

        public List<MatchRound> GetMatchesTournament(int tid) {
            var consulta = from match in this.context.Matches
                           join round in this.context.Rounds on match.Rid equals round.Rid
                           where round.Tid == tid
                           select new MatchRound {
                               Mid = match.Mid,
                               Tblue = match.Tblue,
                               Rblue = match.Rblue,
                               Tred = match.Tred,
                               Rred = match.Rred,
                               Date = match.Date,
                               Fase = round.Name
                           };
            return consulta.ToList();
        }

        public List<TournamentPlayers> GetTournamentWinner(int tid) {
            string sql = "SP_GETGANADOR_TOURNAMENT @TID";
            SqlParameter pamTid = new SqlParameter("@TID", tid);

            var consulta = this.context.TournamentPlayers.FromSqlRaw(sql, pamTid);
            return consulta.AsEnumerable().ToList();
        }

        public void InscriptionPlayerTeamAle(int tid, string uid) {
            string sql = "SP_INSCRIPTION_PLAYER_TEAMALE @TID,@UID";
            SqlParameter[] pams = new SqlParameter[] {
                new SqlParameter("@TID", tid),
                new SqlParameter("@UID", uid)
            };

            this.context.Database.ExecuteSqlRaw(sql, pams);
        }

        public bool ValidateInscription(int tid, string uid) {
            var consulta = this.context.TournamentPlayers.FirstOrDefault(z => z.Tid == tid && z.Uid == uid);

            if (consulta != null) {
                return true;
            } else {
                return false;
            }
        }

        public List<TournamentComplete> GetTournamentsUser(string name) {
            var consulta = from data in this.context.TournamentCompletes
                           where data.Organizator == name
                           select data;
            return consulta.ToList();
        }

        public void DeleteTournament(int tid) {
            string sql = "SP_DELETE_TOURNAMENT @TID";
            SqlParameter pamTid = new SqlParameter("@TID", tid);

            this.context.Database.ExecuteSqlRaw(sql, pamTid);
        }

        public async Task<Match> FindMatchAsync(int mid) {
            return await this.context.Matches.FirstOrDefaultAsync(x => x.Mid == mid);
        }


        public async Task DeteleUserTournamentAsync(int tid, string uid) {
            string sql = "SP_DELETE_PLAYER_TOURNAMENT @TID, @UID";
            SqlParameter pamTid = new SqlParameter("@TID", tid);
            SqlParameter pamUid = new SqlParameter("@UID", uid);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamTid, pamUid);
        }

        public async Task UpdateMatchesTournamentAsync(int mid, int rblue, int rred) {
            Match match = await this.FindMatchAsync(mid);
            match.Rblue = rblue;
            match.Rred = rred;
            await this.context.SaveChangesAsync();
        }

        public int TotalMatchesRoundWinner(int rid) {
            var consulta = this.context.Matches.Where(m => m.Rid == rid && (m.Rblue != null && m.Rred != null && m.Rblue != m.Rred)).Count();
            return (int)consulta;
        }

        public int TotalMatchesRound(int rid) {
            var consulta = this.context.Matches.Where(m => m.Rid == rid).Count();
            return (int)consulta;
        }

        public async Task InsertMatchesNextRoundAsync(int rid) {
            string sql = "SP_INSERT_NEXT_MATCHES @RID";
            SqlParameter pamrid = new SqlParameter("@RID", rid);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamrid);
        }



        #endregion
    }
}
