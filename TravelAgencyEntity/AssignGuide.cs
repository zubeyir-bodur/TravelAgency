using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
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
