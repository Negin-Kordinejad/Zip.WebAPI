using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zip.WebAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Display(Name="Email Address")]
        public string Email { get; set; }

        [Range(1,int.MaxValue)]
        [Display(Name = "Monthly-Salary")]
        [Required]
        public int Salary { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        [Display(Name = "Monthly-Expenses")]
        public int Expenses { get; set; }

        public virtual ICollection<Acount> Acounts { get; set; }
    }
}
