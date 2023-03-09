using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTChallenge.Models {
    [Table("V_PLAYERS_TOURNAMENT")]
    public class TournamentPlayers {
        [Key]
        [Column("UID")]
        public string Uid { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("TAG")]
        public string Tag { get; set; }

        [Column("IMAGE_SMALL")]
        public string ImageSmall { get; set; }

        [Column("TEAM")]
        public int Team { get; set; }

        [Column("TID")]
        public int Tid { get; set; }
    }
}
