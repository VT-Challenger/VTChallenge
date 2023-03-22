using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VTChallenge.Models {
    [Table("USERS")]
    public class Users {
        [Key]
        [Column("UID")]
        public string Uid { get; set; }

        [Required(ErrorMessage = "Complete el campo")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Por favor ingrese un email válido")]
        //COMPROBAR SI EXISTE YA EL EMAIL
        //[Remote("CheckEmailAvailability", "Account", HttpMethod = "POST", ErrorMessage = "Este Tag ya está en uso")]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Complete el campo")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un número y un caracter especial")]
        [Column("PASSWORD")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Complete el campo")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Complete el campo")]
        [StringLength(255)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Complete el campo")]
        [StringLength(5, MinimumLength = 3, ErrorMessage = "El Tag debe tener entre 3 y 5 caracteres")]
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
