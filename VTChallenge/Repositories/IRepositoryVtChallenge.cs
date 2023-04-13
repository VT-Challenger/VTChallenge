using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public interface IRepositoryVtChallenge {
        Task<List<Users>> GetUsers();
        Task<Users> FindUserAsync(string uid);
        Task<Users> FindUserByNameAsync(string name);
        Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string imagelarge, string rango);
        Task<Users> LoginNamePasswordAsync(string name, string password);
        Task UpdateProfileAsync(string uid);
        Task<int> GetTotalWinsAsync(string uid);
        Task<List<TournamentComplete>> GetTournaments();
        Task<List<TournamentComplete>> GetTournamentsByRankAsync(string rank);
        Task<List<TournamentComplete>> GetTournamentCompletesFindAsync(string filtro, string rank);
        Task<TournamentComplete> GetTournamentComplete(int tid);
        Task<List<TournamentPlayers>> GetPlayersTournament(int tid);
        Task<List<Round>> GetRounds(int tid);
        Task<List<MatchRound>> GetMatchesTournament(int tid);
        Task<List<TournamentPlayers>> GetTournamentWinner(int tid);
        Task InscriptionPlayerTeamAle(int tid, string uid);
        Task<bool> ValidateInscription(int tid, string uid);
        Task<List<TournamentComplete>> GetTournamentsUser(string name);
        Task<List<TournamentComplete>> GetTournamentsUserFindAsync(string name,string filtro);
        Task DeleteTournament(int tid);
        Task DeteleUserTournamentAsync(int tid, string uid);
        Task UpdateMatchesTournamentAsync(int mid, int rblue, int rred);
        Task<Match> FindMatchAsync(int mid);
        Task<int> TotalMatchesRound(int rid);
        Task<int> TotalMatchesRoundWinner(int rid);
        Task InsertMatchesNextRoundAsync(int rid);
        int GetMaxIdTournament();
        int GetMinIdRoundTournament(int tid);
        Task InsertTournamentAsync(int tid, string name, string rank, DateTime dateinit, string description, int pid, int players, string organizator, string image);
        Task InsertRoundAsync(string name, DateTime date, int tid);
        Task InsertMatchAsync(int tblue, int tred, DateTime time, int rid);
        Task<Round> FindRoundAsync(int rid);
       

    }
}
