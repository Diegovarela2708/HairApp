using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HairApp.Common.Entities
{
    public class Departament
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El {0} El Departamento debe de contener {1} caracteres.")]
        [Required]
        [Display(Name = "Departamento")]
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        [DisplayName("Numero Departamentos")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
