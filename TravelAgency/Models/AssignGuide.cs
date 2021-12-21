using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class AssignGuide
    {
        public int TourId { get; set; }
        public int? GuideUId { get; set; }
        public int? AgentUId { get; set; }
        public string AssignStatus { get; set; }

        public virtual Agent AgentU { get; set; }
        public virtual Guide GuideU { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
