﻿using VTChallenge.Models;

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
        TournamentComplete GetTournamentComplete(int tid);
        List<TournamentPlayers> GetPlayersTournament(int tid);
        List<Round> GetRounds(int tid);
        List<MatchRound> GetMatchesTournament(int tid);
        List<TournamentPlayers> GetTournamentWinner(int tid);
        void InscriptionPlayerTeamAle(int tid, string uid);
        bool ValidateInscription(int tid, string uid);
        List<TournamentComplete> GetTournamentsUser(string name);
        void DeleteTournament(int tid);
        Task DeteleUserTournamentAsync(int tid, string uid);
        Task UpdateMatchesTournament(int mid, int tblue, int tred, int rblue, int rred, DateTime date, int rid);

    }
}
