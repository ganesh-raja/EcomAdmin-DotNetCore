using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomAdmin.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "varchar(200)")]
        public string? ImageUrl { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
