using System.Diagnostics.Metrics;
using System.Xml.Linq;
using VTChallenge.Data;
using VTChallenge.Models;

namespace VTChallenge.Repositories {

    #region PROCEDURES
    //CREATE PROCEDURE SP_REGISTER_USER
    //(@UID NVARCHAR(250),@NAME NVARCHAR(50), @TAG NVARCHAR(50), @EMAIL NVARCHAR(250), @PASSWORD NVARCHAR(20), @IMAGESMALL NVARCHAR(250), @IMAGELARGE NVARCHAR(250), @RANK NVARCHAR(25))
    //AS
    //    INSERT INTO USERS VALUES(@UID, @NAME, @TAG, @EMAIL, @PASSWORD, @IMAGESMALL, @IMAGELARGE, @RANK);
    //GO
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

        public async Task RegisterUserAsync(string uid, string name, string tag, string email, string password, string imagesmall, string iamgelarge, string rank) {
            Users user = new Users() {
                Uid = uid,
                Name = name,
                Tag = tag,
                Email = email,
                Password = password,
                ImageSmall = imagesmall,
                ImageLarge = iamgelarge,
                Rank = rank
            };
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }
    }
}
