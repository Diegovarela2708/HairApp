using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        [Display(Name = "Puntaje Servico")]
        public float Score { get; set; }
        [Display(Name = "Descriptión")]
        public string Description { get; set; }
        public BookingHistory BookingHistory { get; set; }
    }
}
