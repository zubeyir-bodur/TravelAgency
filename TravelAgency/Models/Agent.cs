using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Agent
    {
        public Agent()
        {
            AssignGuide = new HashSet<AssignGuide>();
        }

        public int UId { get; set; }

        public virtual Employee U { get; set; }
        public virtual ICollection<AssignGuide> AssignGuide { get; set; }
    }
}
