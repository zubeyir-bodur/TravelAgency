using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class HotelReservation
    {
        public int ReserveId { get; set; }
        public DateTime? ReserveEndDate { get; set; }
        public int? RoomId { get; set; }
        public int? HotelId { get; set; }

        public virtual Reservation Reserve { get; set; }
        public virtual Room Room { get; set; }
    }
}
