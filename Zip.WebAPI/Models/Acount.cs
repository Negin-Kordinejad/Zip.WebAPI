using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Acount Type")]
        public string Type { get; set; }

        public virtual User User { get; set; }
    }
}
