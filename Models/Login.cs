using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomAdmin.Models
{
    public class Login
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Username")]
        [Column(TypeName = "varchar(50)")]
        public string uname { get; set; } = null!;
        [Required]
        [Display(Name = "Password")]
        [Column(TypeName = "varchar(50)")]
        public string pass {  get; set; } = null!;
        [Required]
        public Boolean? access {  get; set; }
        public DateTime createddate { get; set; }        
        
    }
}
