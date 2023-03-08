using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VTChallenge.Models {
    [Table("USERS")]
    public class Users {
        [Key]
        [Column("UID")]
        public string Uid { get; set; }

        [Required]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required]
        [Column("PASSWORD")]
        public string Password { get; set; }

        [Required]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("TAG")]
        public string Tag { get; set; }

        [Column("IMAGE_SMALL")]
        public string? ImageSmall { get; set; }

        [Column("IMAGE_LARGE")]
        public string? ImageLarge { get; set; }

        [Column("RANK")]
        public string? Rank { get; set; }

        [Column("ROL")]
        public string? Rol { get; set; }

        [Column("SALT")]
        public string Salt { get; set; }

        [Column("PASSENCRIPT")]
        public byte[] PassEncript { get; set; }

        public Users() { }

    }
}
