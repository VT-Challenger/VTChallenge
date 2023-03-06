using VTChallenge.Data;
using VTChallenge.Models;

namespace VTChallenge.Repositories {
    public class RepositoryTournaments : IRepositoryTournaments {

        VTChallengeContext context;
        
        public RepositoryTournaments(VTChallengeContext context) {
            this.context = context;
        }


        public List<Tournament> GetTournaments() {
            var consulta = from data in this.context.Tournaments
                           select data;

            return consulta.ToList();
        }
    }
}
