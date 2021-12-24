using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class SightseeingPlace
    {
        public SightseeingPlace()
        {
            Visits = new HashSet<Visit>();
        }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string SLocation { get; set; }
        public string SDescription { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
