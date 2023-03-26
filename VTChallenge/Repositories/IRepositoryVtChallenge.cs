using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public interface IRepositoryVtChallenge {
        List<Users> getUser();
        Task<Users> FindUserAsync(string uid);
        Task<Users> FindUserByNameAsync(string name);
        Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string imagelarge, string rango);
        Task<Users> LoginNamePasswordAsync(string name, string password);
        Task UpdateProfileAsync(string uid);
        Task<int> GetTotalWinsAsync(string uid);
        List<TournamentComplete> GetTournaments();
        Task<List<TournamentComplete>> GetTournamentsByRankAsync(string rank);
        Task<List<TournamentComplete>> GetTournamentCompletesFindAsync(string filtro, string rank);
       TournamentComplete GetTournamentComplete(int tid);
        List<TournamentPlayers> GetPlayersTournament(int tid);
        List<Round> GetRounds(int tid);
        List<MatchRound> GetMatchesTournament(int tid);
        List<TournamentPlayers> GetTournamentWinner(int tid);
        void InscriptionPlayerTeamAle(int tid, string uid);
        bool ValidateInscription(int tid, string uid);
        List<TournamentComplete> GetTournamentsUser(string name);
        Task<List<TournamentComplete>> GetTournamentsUserFindAsync(string name,string filtro);
        void DeleteTournament(int tid);
        Task DeteleUserTournamentAsync(int tid, string uid);
        Task UpdateMatchesTournamentAsync(int mid, int rblue, int rred);
        Task<Match> FindMatchAsync(int mid);
        int TotalMatchesRound(int rid);
        int TotalMatchesRoundWinner(int rid);
        Task InsertMatchesNextRoundAsync(int rid);
        int GetMaxIdTournament();
        int GetMinIdRoundTournament(int tid);
        Task InsertTournamentAsync(int tid, string name, string rank, DateTime dateinit, string description, int pid, int players, string organizator, string image);
        Task InsertRoundAsync(string name, DateTime date, int tid);
        Task InsertMatchAsync(int tblue, int tred, DateTime time, int rid);
        Task<Round> FindRoundAsync(int rid);
       

    }
}
