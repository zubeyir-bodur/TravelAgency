using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class MarkedActivity
    {
        public int ReserveId { get; set; }
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual TourReservation Reserve { get; set; }
    }
}
