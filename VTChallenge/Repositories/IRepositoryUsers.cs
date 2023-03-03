using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public interface IRepositoryUsers {
        List<Users> getUser();
        Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string imagelarge, string rank);
    }
}
