using System.ComponentModel.DataAnnotations;

namespace EcomAdmin.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
