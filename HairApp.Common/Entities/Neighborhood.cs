using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HairApp.Common.Entities
{
    public class Neighborhood
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El {0} Barrio debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Barrio")]
        public string Name { get; set; }      

        [JsonIgnore]
        [NotMapped]
        public int IdCity { get; set; }
        [JsonIgnore]
        public City City { get; set; }
    }
}
