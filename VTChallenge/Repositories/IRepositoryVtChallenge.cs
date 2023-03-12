using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public interface IRepositoryVtChallenge {
        List<Users> getUser();
        Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string imagelarge, string rango);
        Users LoginNamePassword(string name, string password);
        List<TournamentComplete> GetTournaments();
        TournamentComplete GetTournamentComplete(int tid);
        List<TournamentPlayers> GetPlayersTournament(int tid);
        List<Round> GetRounds(int tid);
        List<MatchRound> GetMatchesTournament(int tid);
        List<TournamentPlayers> GetTournamentWinner(int tid);
        void InscriptionPlayerTeamAle(int tid, string uid);
        bool ValidateInscription(int tid, string uid);
        List<TournamentComplete> GetTournamentsUser(string name);
        void DeleteTournament(int tid);

    }
}
