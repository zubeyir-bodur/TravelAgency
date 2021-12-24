using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Activity
    {
        public Activity()
        {
            MarkedActivities = new HashSet<MarkedActivity>();
        }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ADescription { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public decimal? TicketPrice { get; set; }
        public int TourId { get; set; }
        public int? DiscountId { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Concert Concert { get; set; }
        public virtual Festival Festival { get; set; }
        public virtual ICollection<MarkedActivity> MarkedActivities { get; set; }
    }
}
