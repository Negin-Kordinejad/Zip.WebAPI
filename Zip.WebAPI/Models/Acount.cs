using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.WebAPI.Models
{
    public class Acount
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Acount Type")]
        [Column(TypeName = "varchar(10)")]
        public string Type { get; set; }

        public virtual User User { get; set; }
    }
}
