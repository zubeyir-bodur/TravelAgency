using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Room
    {
        public Room()
        {
            HotelReservation = new HashSet<HotelReservation>();
        }

        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public int? Number { get; set; }
        public int? Size { get; set; }
        public decimal? Price { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<HotelReservation> HotelReservation { get; set; }
    }
}
