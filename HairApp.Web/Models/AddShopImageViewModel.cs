using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HairApp.Web.Models
{
    public class AddShopImageViewModel
    {


        public int ShopId { get; set; }

        [Display(Name = "Image")]
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
