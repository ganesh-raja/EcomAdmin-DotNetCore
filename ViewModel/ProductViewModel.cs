using EcomAdmin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcomAdmin.ViewModel
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}
