using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Guide
    {
        public Guide()
        {
            AssignGuides = new HashSet<AssignGuide>();
            GuideReviews = new HashSet<GuideReview>();
            GuidesFeedbacks = new HashSet<GuidesFeedback>();
            Speaks = new HashSet<Speak>();
        }

        public int UId { get; set; }

        public virtual Employee UIdNavigation { get; set; }
        public virtual ICollection<AssignGuide> AssignGuides { get; set; }
        public virtual ICollection<GuideReview> GuideReviews { get; set; }
        public virtual ICollection<GuidesFeedback> GuidesFeedbacks { get; set; }
        public virtual ICollection<Speak> Speaks { get; set; }
    }
}
