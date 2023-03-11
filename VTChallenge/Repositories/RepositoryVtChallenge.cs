using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using VTChallenge.Data;
using VTChallenge.Helpers;
using VTChallenge.Models;
using VTChallenge.Services;

namespace VTChallenge.Repositories {

    #region PROCEDURES

    #endregion

    public class RepositoryVtChallenge : IRepositoryVtChallenge {

        private VTChallengeContext context;

        public RepositoryVtChallenge(VTChallengeContext context) {
            this.context = context;
        }

        #region METHODS USERS
        public List<Users> getUser() {
            var consulta = from data in this.context.Users
                           select data;
            return consulta.ToList();
        }

        public Users LoginNamePassword(string username, string password) {
            Users user = this.context.Users.FirstOrDefault(u => u.Name == username);
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

        public void InscriptionPlayerTeamAleASync(int tid, string uid) {
            string sql = "SP_INSCRIPTION_PLAYER_TEAMALE @TID,@UID";
            SqlParameter[] pams = new SqlParameter[] {
                new SqlParameter("@TID", tid),
                new SqlParameter("@UID", uid)
            };

            this.context.Database.ExecuteSqlRaw(sql, pams);
        }
        #endregion
    }
}
