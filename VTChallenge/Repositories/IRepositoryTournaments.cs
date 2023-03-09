using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public interface IRepositoryTournaments {
        List<TournamentComplete> GetTournaments();
        TournamentComplete GetTournamentComplete(int tid);
        List<TournamentPlayers> GetPlayersTournament(int tid);
    }
}
