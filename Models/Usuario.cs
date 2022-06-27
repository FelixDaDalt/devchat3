using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace devchat3.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "VARCHAR(30)")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(500)")]
        public string password { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(500)")]
        public string confirmar { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string fullname { get; set; }

        [DataType(DataType.Date)]
        public DateTime nac { get; set; }

        public bool isFacebook { get; set; } = false;
        public bool isGoogle { get; set; } = false;

        public string photo { get; set; } = null;

    }
}
