using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class TourReservation
    {
        public TourReservation()
        {
            MarkedActivity = new HashSet<MarkedActivity>();
        }

        public int ReserveId { get; set; }
        public int? TourId { get; set; }

        public virtual Reservation Reserve { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<MarkedActivity> MarkedActivity { get; set; }
    }
}
