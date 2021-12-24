using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Discount
    {
        public Discount()
        {
            Activities = new HashSet<Activity>();
            Hotels = new HashSet<Hotel>();
            Tours = new HashSet<Tour>();
        }

        public int DiscountId { get; set; }
        public int? Percents { get; set; }
        public int? DiscountTimeInterval { get; set; }
        public DateTime? DiscountStartTime { get; set; }
        public string DiscountType { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
