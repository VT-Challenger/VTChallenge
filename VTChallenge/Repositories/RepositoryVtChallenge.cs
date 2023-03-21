using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
            return await this.context.Users.FirstOrDefaultAsync(x=> x.Uid == uid);
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
            string sql = "SP_GETTOTAL_TOURNAMENTS_WON @UID, @TOTALWINS OUT";
            SqlParameter pamuid = new SqlParameter("@UID", uid);
            SqlParameter pamTotalWins = new SqlParameter("@TOTALWINS",-1);
            pamTotalWins.Direction = ParameterDirection.Output;

            //await this.context.Database

            //return (int)returnValue.Value;
            return 0;
        }

        public async Task UpdateProfileAsync(string uid) {
            DataApi data = null;
            UserApi userapi = await this.api.GetAccountUidAsync(uid);
            Users user = await this.FindUserAsync(uid);

            if(userapi != null) {
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
                               Rred= match.Rred,
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

    
        #endregion
    }
}
