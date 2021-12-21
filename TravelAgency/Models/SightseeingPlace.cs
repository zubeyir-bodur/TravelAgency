using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class SightseeingPlace
    {
        public SightseeingPlace()
        {
            Visits = new HashSet<Visits>();
        }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string SLocation { get; set; }
        public string SDescription { get; set; }

        public virtual ICollection<Visits> Visits { get; set; }
    }
}
