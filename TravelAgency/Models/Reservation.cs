using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Reservation
    {
        public int ReserveId { get; set; }
        public DateTime? ReserveStartDate { get; set; }
        public int? NumReserving { get; set; }
        public bool? IsBooked { get; set; }
        public int? UId { get; set; }

        public virtual Customer U { get; set; }
        public virtual HotelReservation HotelReservation { get; set; }
        public virtual TourReservation TourReservation { get; set; }
    }
}
