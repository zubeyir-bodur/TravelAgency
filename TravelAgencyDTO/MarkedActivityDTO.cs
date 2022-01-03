using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class MarkedActivityDTO
    {
        public int tourId { get; set; }
        public string activityName { get; set; }
        public DateTime activityStartTime { get; set; }
        public DateTime activityEndTime { get; set; }
        public int numReserving { get; set; }
        public string tourName { get; set; }
        public int ticketPrice { get; set; }
        public bool isBooked { get; set; }
    }
}
