using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class MarkedActivity
    {
        public int ReserveId { get; set; }
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual TourReservation Reserve { get; set; }
    }
}
