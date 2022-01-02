using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class ActivityDTO
    {
        public int activityId { get; set; }
        public string activityName { get; set; }
        public string aDescription { get; set; }
        public DateTime? activityStartTime { get; set; }
        public DateTime? activityEndTime { get; set; }
        public decimal ticketPrice { get; set; }
        public int discountPercents { get; set; }
    }
}
