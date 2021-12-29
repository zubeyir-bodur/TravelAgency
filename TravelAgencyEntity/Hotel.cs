using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public int? NumOfStars { get; set; }
        public int? DiscountId { get; set; }
        public DateTime? DiscountStartDate { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
