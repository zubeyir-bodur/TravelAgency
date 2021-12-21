using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class TourReview
    {
        public int ReviewId { get; set; }
        public int TourId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
