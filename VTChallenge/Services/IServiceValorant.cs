using VTChallenge.Models.Api;

namespace VTChallenge.Services
{
    public interface IServiceValorant {

        Task<UserApi> GetAccountAsync(string username, string tagline);
        Task<string> GetRankAsync(string username, string tag); 

    }
}
