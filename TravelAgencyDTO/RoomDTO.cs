using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class RoomDTO
    {
        public int roomId { get; set; }
        public int number { get; set; }
        public int size { get; set; }
        public decimal price { get; set; }
        public decimal priceDiscounted { get; set; }
    }
}
