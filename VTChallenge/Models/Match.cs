using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTChallenge.Models {
    [Table("MATCH")]
    public class Match {
        [Key]
        [Column("MID")]
        public int Mid { get; set; }

        [Column("TBLUE")]
        public int Tblue { get; set; }

        [Column("TRED")]
        public int Tred { get; set; }

        [Column("TBLUEKDA")]
        public float TblueKDA { get; set; }

        [Column("TREDKDA")]
        public float TredKDA { get; set; }

        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("RID")]
        public int Rid { get; set; }


    }
}
