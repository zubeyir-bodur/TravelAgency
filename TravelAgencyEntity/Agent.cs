using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Agent
    {
        public Agent()
        {
            AssignGuides = new HashSet<AssignGuide>();
        }

        public int UId { get; set; }

        public virtual Employee UIdNavigation { get; set; }
        public virtual ICollection<AssignGuide> AssignGuides { get; set; }
    }
}
