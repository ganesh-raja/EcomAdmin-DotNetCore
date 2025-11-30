using EcomAdmin.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EcomAdmin.ViewModel
{
    public class AdminViewModel
    {
        public Login NewAdmin { get; set; }
        [ValidateNever]
        public List<Login> AdminList { get; set; }
    }
}
