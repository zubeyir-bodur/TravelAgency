using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Guide
    {
        public Guide()
        {
            AssignGuide = new HashSet<AssignGuide>();
            GuideReview = new HashSet<GuideReview>();
            GuidesFeedback = new HashSet<GuidesFeedback>();
            Speak = new HashSet<Speak>();
        }

        public int UId { get; set; }

        public virtual Employee U { get; set; }
        public virtual ICollection<AssignGuide> AssignGuide { get; set; }
        public virtual ICollection<GuideReview> GuideReview { get; set; }
        public virtual ICollection<GuidesFeedback> GuidesFeedback { get; set; }
        public virtual ICollection<Speak> Speak { get; set; }
    }
}
