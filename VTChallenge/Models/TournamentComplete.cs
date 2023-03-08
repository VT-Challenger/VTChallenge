using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#region VIEW
//CREATE VIEW V_LIST_TOURNAMENTS
//AS
//	SELECT TOURNAMENT.TID, TOURNAMENT.NAME, TOURNAMENT.DATEINIT, TOURNAMENT.DESCRIPTION,
//    PLATFORM.NAME AS PLATFORM, PLATFORM.IMAGE AS PLATFORM_IMAGE, TOURNAMENT.PLAYERS AS LIMIT_PLAYERS, USERS.NAME AS ORGANIZATOR, USERS.IMAGE_LARGE AS IMAGE_ORGANIZATOR,
//    TOURNAMENT.IMAGE, COUNT(TOURNAMENT_PLAYERS.UID) AS INSCRIPTIONS
//	FROM TOURNAMENT
//	INNER JOIN  PLATFORM on TOURNAMENT.PID = PLATFORM.PID
//	INNER JOIN USERS on TOURNAMENT.ORGANIZATOR = USERS.UID
//	INNER JOIN TOURNAMENT_PLAYERS ON TOURNAMENT.TID = TOURNAMENT_PLAYERS.TID
//	GROUP BY TOURNAMENT.TID, TOURNAMENT.NAME, TOURNAMENT.DATEINIT, TOURNAMENT.DESCRIPTION,
//    PLATFORM.NAME, PLATFORM.IMAGE, TOURNAMENT.PLAYERS, USERS.NAME, USERS.IMAGE_LARGE,
//    TOURNAMENT.IMAGE
//GO
#endregion

namespace VTChallenge.Models {

    [Table("V_TOURNAMENT_COMPLETE")]
    public class TournamentComplete {
        [Key]
        [Column("TID")]
        public int Tid { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("TRANK")]
        public string? Rank { get; set; }

        [Column("DATEINIT")]
        public DateTime DateInit { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("PLATFORM")]
        public string Platform { get; set; }

        [Column("PLATFORM_IMAGE")]
        public string PlatformImage { get; set; }

        [Column("LIMIT_PLAYERS")]
        public int LimitPlayers { get; set; }

        [Column("ORGANIZATOR")]
        public string Organizator { get; set; }

        [Column("IMAGE_ORGANIZATOR")]
        public string ImageOrganization { get; set; }

        [Column("IMAGE")]
        public string? Image { get; set; }

        [Column("INSCRIPTIONS")]
        public int Inscriptions { get; set; }

    }
}
