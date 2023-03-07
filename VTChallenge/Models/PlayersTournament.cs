using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#region VIEW
//CREATE VIEW V_PLAYERS_TOURNAMENT
//AS
//	SELECT USERS.UID, USERS.NAME, USERS.TAG, USERS.IMAGE_SMALL,
//    TOURNAMENT_PLAYERS.TEAM,
//    TOURNAMENT.TID
//	FROM USERS
//	INNER JOIN  TOURNAMENT_PLAYERS on USERS.UID = TOURNAMENT_PLAYERS.UID
//	INNER JOIN  TOURNAMENT on TOURNAMENT_PLAYERS.TID = TOURNAMENT.TID
//GO
#endregion

namespace VTChallenge.Models {
    public class PlayersTournament {

        [Key]
        [Column("UID")]
        public string Uid { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("TAG")]
        public string Tag { get; set; }

        [Column("IMAGE_SMALL")]
        public string? ImageSmall { get; set; }

        [Column("TEAM")]
        public string Team { get; set; }

        [Column("TID")]
        public int Tid { get; set; }

    }
}
