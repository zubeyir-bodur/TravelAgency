using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string Content { get; set; }
        public DateTime? FeedbackTime { get; set; }

        public virtual GuidesFeedback GuidesFeedback { get; set; }
    }
}
