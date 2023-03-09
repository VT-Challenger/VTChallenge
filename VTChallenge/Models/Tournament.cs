using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTChallenge.Models {
    [Table("TOURNAMENT")]
    public class Tournament {
        [Key]
        [Column("TID")]
        public int Tid { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("RANK")]
        public string? Rank { get; set; }

        [Column("DATEINIT")]
        public DateTime DateInit { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("PID")]
        public string Platform { get; set; }

        [Column("PALYERS")]
        public string Players { get; set; }

        [Column("ORGANIZATOR")]
        public string Organizator { get; set; }

        [Column("IMAGE")]
        public string? Image { get; set; }
    }
}
