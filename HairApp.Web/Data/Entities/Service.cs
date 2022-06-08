using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HairApp.Web.Data.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Servicio")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Descripción")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Tiempo del servicio")]
        [Required]
        public int ServiceTime { get; set; }
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public int BookingsNumber => Bookings == null ? 0 : Bookings.Count;
        public ICollection<BookingHistory> BookingHistories { get; set; }
        public int BookingHistoriesNumber => BookingHistories == null ? 0 : BookingHistories.Count;

        [JsonIgnore]
        [NotMapped]
        public int IdShop { get; set; }
        [JsonIgnore]
        public Shop Shop { get; set; }

    }
}
