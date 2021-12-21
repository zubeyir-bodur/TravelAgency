using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Room = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public int? NumOfStars { get; set; }
        public int? DiscountId { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }
}
