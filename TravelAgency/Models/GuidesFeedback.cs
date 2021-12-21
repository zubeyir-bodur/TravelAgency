using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class GuidesFeedback
    {
        public int FeedbackId { get; set; }
        public int? UId { get; set; }
        public int? TourId { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Guide U { get; set; }
    }
}
