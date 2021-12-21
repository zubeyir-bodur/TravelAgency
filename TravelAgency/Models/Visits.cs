using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Visits
    {
        public int PlaceId { get; set; }
        public int TourId { get; set; }

        public virtual SightseeingPlace Place { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
