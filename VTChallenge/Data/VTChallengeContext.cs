using Microsoft.EntityFrameworkCore;
using VTChallenge.Models;

namespace VTChallenge.Data {
    public class VTChallengeContext : DbContext {
        public VTChallengeContext(DbContextOptions<VTChallengeContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}
