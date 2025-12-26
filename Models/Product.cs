using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomAdmin.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        [Column(TypeName = "varchar(100)")]
        public string? Name { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Description { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string? ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }               
        public DateTime CreatedDate { get; set; }

    }
}
