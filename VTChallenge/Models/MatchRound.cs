using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTChallenge.Models {
    public class MatchRound {

        public int Mid { get; set; }

        public int Tblue { get; set; }

        public int Tred { get; set; }

        public int Rblue { get; set; }

        public int Rred { get; set; }

        public DateTime Date { get; set; }

        public string Fase { get; set; }
    }
}
