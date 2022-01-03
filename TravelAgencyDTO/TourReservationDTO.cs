using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class TourReservationDTO
    {
        public int reserveId { get; set; }
        public DateTime reserveStartDate { get; set; }
        public DateTime reserveEndDate { get; set; }
        public int numReserving { get; set; }
        public string tourName { get; set; }
        public decimal price { get; set; }
        public bool isBooked { get; set; }
    }
}
