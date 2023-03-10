using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTChallenge.Models {
    [Table("ROUND")]
    public class Round {
        [Key]
        [Column("RID")]
        public int Rid { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("TID")]
        public int Tid { get; set; }

    }
}
