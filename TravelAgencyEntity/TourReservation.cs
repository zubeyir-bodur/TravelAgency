using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class TourReservation
    {
        public TourReservation()
        {
            MarkedActivities = new HashSet<MarkedActivity>();
        }

        public int ReserveId { get; set; }
        public int? TourId { get; set; }

        public virtual Reservation Reserve { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<MarkedActivity> MarkedActivities { get; set; }
    }
}
