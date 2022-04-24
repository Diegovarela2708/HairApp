﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Models
{
    public class CreateShopViewModel
    {
        [Required]
        [Display(Name = "Departamento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un departamento.")]
        public int DepartamentId { get; set; }

        public IEnumerable<SelectListItem> Departaments { get; set; }

        [Required]
        [Display(Name = "Pais")]
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
