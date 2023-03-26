using Microsoft.EntityFrameworkCore;
using VTChallenge.Models;

namespace VTChallenge.Data {
    public class VTChallengeContext : DbContext {
        public VTChallengeContext(DbContextOptions<VTChallengeContext> options) : base(options) { }

        #region TABLAS
        public DbSet<Users> Users { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        #endregion

        #region VISTAS
        public DbSet<TournamentComplete> TournamentCompletes { get; set; }
        public DbSet<TournamentPlayers> TournamentPlayers { get; set; }
        #endregion 
    }
}
