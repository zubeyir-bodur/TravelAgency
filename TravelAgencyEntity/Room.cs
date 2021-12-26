using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Room
    {
        public Room()
        {
            HotelReservations = new HashSet<HotelReservation>();
        }

        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public int? Number { get; set; }
        public int? Size { get; set; }
        public decimal? Price { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<HotelReservation> HotelReservations { get; set; }
    }
}
