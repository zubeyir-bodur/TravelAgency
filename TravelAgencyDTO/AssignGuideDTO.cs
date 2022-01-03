using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class TourReservationDTO
    {
        public int tourId { get; set; }
        public int? guideUId { get; set; }
        public int? agentUId { get; set; }
        public string assignStatus { get; set; }
    }
}
