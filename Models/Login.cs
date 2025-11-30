using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace EcomAdmin.Models
{
    public class Login
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string uname { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string pass {  get; set; }
        [Required]
        public Boolean? access {  get; set; }
        public DateTime createddate { get; set; }        
        
    }
}
