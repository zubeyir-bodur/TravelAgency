using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Visit
    {
        public int PlaceId { get; set; }
        public int TourId { get; set; }

        public virtual SightseeingPlace Place { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
