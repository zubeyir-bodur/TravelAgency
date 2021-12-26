using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class GuidesFeedback
    {
        public int FeedbackId { get; set; }
        public int? UId { get; set; }
        public int? TourId { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Guide UIdNavigation { get; set; }
    }
}
