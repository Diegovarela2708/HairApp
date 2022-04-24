using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DateLocal { get; set; }
        public User User { get; set; }
        public char Status { get; set; }
        public string Addrees { get; set; }
        public int MyProperty { get; set; }        

    }
}
