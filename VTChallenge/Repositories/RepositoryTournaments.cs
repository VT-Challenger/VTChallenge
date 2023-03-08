using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using VTChallenge.Data;
using VTChallenge.Models;


#region PROCEDURE
//CREATE PROCEDURE SP_GET_INSCRIPTION(@TID INT)
//AS
//    select COUNT(UID) as TOTAL_INSCRIPTION from TOURNAMENT_PLAYERS where TID = @TID;
//GO
#endregion

namespace VTChallenge.Repositories {
    public class RepositoryTournaments : IRepositoryTournaments {

        VTChallengeContext context;

        public RepositoryTournaments(VTChallengeContext context) {
            this.context = context;
        }


        public List<TournamentComplete> GetTournaments() {
            var consulta = from data in this.context.TournamentCompletes
                           select data;

            return consulta.ToList();
        }
    }
}
