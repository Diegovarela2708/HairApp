﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceTime { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public int BookingsNumber => Bookings == null ? 0 : Bookings.Count;
        public ICollection<BookingHistory> BookingHistories { get; set; }
        public int BookingHistoriesNumber => BookingHistories == null ? 0 : BookingHistories.Count;

    }
}