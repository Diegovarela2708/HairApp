using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HairApp.Common.Entities;


namespace HairApp.Web.Data.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Addrees { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public float Balance { get; set; }
        public float StarCalification { get; set; }
        public ICollection<Service> Services { get; set; }
        public int ServicesNumber => Services == null ? 0 : Services.Count;


    }
}
