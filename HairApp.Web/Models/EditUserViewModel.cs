using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HairApp.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [MaxLength(20)]
        [Required]
        [Display(Name = "Documento")]
        public String Document { get; set; }

        [Display(Name = "Nombre(S)")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Apellido(S)")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Dirección")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Tefono")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }
        //TODO: Cambiar Imaguen
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            //? $"https://algamarsa.azurewebsites.net/images/noimage.png"
            ? $"https://localhost:44352/images/noimage.png"
            : $"https://hairapp.blob.core.windows.net/user/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        

        [Required]
        [Display(Name = "Departamento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un departamento.")]
        public int DepartamentId { get; set; }

        public IEnumerable<SelectListItem> Departaments { get; set; }   

        [Required]
        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una ciudad.")]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        [Required]
        [Display(Name = "Barrio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un barrio.")]
        public int NeighborhoodId { get; set; }
        public IEnumerable<SelectListItem> Neighborhoods { get; set; }
        
    }

}
