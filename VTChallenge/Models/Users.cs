using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VTChallenge.Models {
    [Table("USERS")]
    public class Users {
        [Key]
        [Column("UID")]
        public string Uid { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("TAG")]
        public string Tag { get; set; }

        [Column("IMAGE_SMALL")]
        public string ImageSmall { get; set; }

        [Column("IMAGE_LARGE")]
        public string ImageLarge { get; set; }

        [Column("RANK")]
        public string? Rank { get; set; }

        public Users() { }

        public Users(string email, string name, string tag, string imagesmall, string imagelarge, string rank) {
            this.Email = email;
            this.Name = name;
            this.Tag = tag;
            this.ImageSmall = imagesmall;
            this.ImageLarge = imagelarge;
            this.Rank = rank;
        }

    }
}
