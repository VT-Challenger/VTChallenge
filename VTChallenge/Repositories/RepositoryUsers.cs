using System.Diagnostics.Metrics;
using System.Xml.Linq;
using VTChallenge.Data;
using VTChallenge.Helpers;
using VTChallenge.Models;
using VTChallenge.Services;

namespace VTChallenge.Repositories {

    #region PROCEDURES
    
    #endregion

    public class RepositoryUsers : IRepositoryUsers {

        private VTChallengeContext context;
        private IServiceValorant api;
    
        public RepositoryUsers(VTChallengeContext context, IServiceValorant api) {
            this.context = context;
            this.api = api;
        }

        public List<Users> getUser() {
            var consulta = from data in this.context.Users
                           select data;
            return consulta.ToList();
        }

        public Users LoginNamePassword(string username, string password) {
            Users user = this.context.Users.FirstOrDefault(u => u.Name == username);
            if(user == null) {
                return null;
            } else {
                byte[] passUsuario = user.PassEncript;
                string salt = user.Salt;

                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);

                if (respuesta == true) {
                    return user;
                } else {
                    return null;
                }
            }
        }

        public async Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string imagelarge) {
            Users user = new Users();

            user.Uid = uid;
            user.Name = name;
            user.Tag = tag;
            user.Email = email;
            user.Password = password;
            user.ImageSmall = imagesmall;
            user.ImageLarge = imagelarge;
            user.Rank = await this.api.GetRankAsync(name, tag);
            user.Rol = "player";
            user.Salt = HelperCryptography.GenerateSalt();
            user.PassEncript = HelperCryptography.EncryptPassword(password, user.Salt);

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }
    }
}
