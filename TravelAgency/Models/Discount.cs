using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Activity = new HashSet<Activity>();
            Hotel = new HashSet<Hotel>();
            Tour = new HashSet<Tour>();
        }

        public int DiscountId { get; set; }
        public int? Percents { get; set; }
        public int? DiscountTimeInterval { get; set; }
        public DateTime? DiscountStartTime { get; set; }
        public string DiscountType { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<Tour> Tour { get; set; }
    }
}
