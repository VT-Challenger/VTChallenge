using System.Diagnostics.Metrics;
using System.Xml.Linq;
using VTChallenge.Data;
using VTChallenge.Models;

namespace VTChallenge.Repositories {

    #region PROCEDURES
    
    #endregion

    public class RepositoryUsers : IRepositoryUsers {

        VTChallengeContext context;

        public RepositoryUsers(VTChallengeContext context) {
            this.context = context;
        }

        public List<Users> getUser() {
            var consulta = from data in this.context.Users
                           select data;
            return consulta.ToList();
        }

        public Users LoginNamePassword(string name, string password) {
            var consulta = from data in this.context.Users
                           where data.Name == name && data.Password == password
                           select data;
            return consulta.FirstOrDefault();
        }

        public async Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string iamgelarge, string rank) {
            Users user = new Users();

            user.Uid = uid;
            user.Name = name;
            user.Tag = tag;
            user.Email = email;
            user.Password = password;
            user.ImageSmall = imagesmall;
            user.ImageLarge = iamgelarge;
            user.Rank = rank;
            user.Rol = "player";


            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }
    }
}
