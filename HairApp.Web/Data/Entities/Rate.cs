using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Description { get; set; }
        public BookingHistory BookingHistory { get; set; }
    }
}
