using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class HotelDTO
    {
        public int hotelId { get; set; }
        public string hotelName { get; set; }
        public string city { get; set; }
        public int numOfStars { get; set; }
        public int discountPercents { get; set; }
    }
}
