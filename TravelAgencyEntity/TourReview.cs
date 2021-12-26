using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class TourReview
    {
        public int ReviewId { get; set; }
        public int TourId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
