﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HairApp.Web.Data.Entities
{
    public class BookingHistory
    {
        public int Id { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Fecha")]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Fecha Fin")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime EndDateLocal => Date.ToLocalTime();
        public User User { get; set; }
        public char Status { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdService { get; set; }
        [JsonIgnore]
        public Service Service { get; set; }

    }
}
