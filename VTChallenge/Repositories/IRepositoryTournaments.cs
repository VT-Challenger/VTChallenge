using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public interface IRepositoryTournaments {
        List<TournamentComplete> GetTournaments();
    }
}
