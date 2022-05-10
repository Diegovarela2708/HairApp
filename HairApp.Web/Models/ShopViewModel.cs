using HairApp.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Models
{
    public class ShopViewModel : Shop
    {
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

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
