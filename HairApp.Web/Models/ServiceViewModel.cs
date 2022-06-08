using HairApp.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Models
{
    public class ServiceViewModel:Service
    {
        public int ShopId { get; set; }
        public string Time { get; set; }
    }
}
