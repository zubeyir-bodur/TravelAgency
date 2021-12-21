using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string Content { get; set; }
        public DateTime? FeedbackTime { get; set; }

        public virtual GuidesFeedback GuidesFeedback { get; set; }
    }
}
