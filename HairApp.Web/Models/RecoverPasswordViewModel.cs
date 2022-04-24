using System.ComponentModel.DataAnnotations;

namespace HairApp.Web.Models
{
    public class RecoverPasswordViewModel
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
