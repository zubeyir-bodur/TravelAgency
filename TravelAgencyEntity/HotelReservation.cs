using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
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
