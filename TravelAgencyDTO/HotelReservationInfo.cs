using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class HotelReservationInfo
    {
        public DateTime tourStartDate { get; set; }
        public DateTime tourEndDate { get; set; }
        public int numPeople { get; set; }
        public bool isBooked { get; set; }
        public int uId { get; set; }
        public int roomId { get; set; }
        public int hotelId { get; set; }
    }
}
