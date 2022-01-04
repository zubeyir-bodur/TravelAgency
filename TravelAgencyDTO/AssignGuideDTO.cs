using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class AssignGuideDTO
    {
        public int tourId { get; set; }
        public int? guideUId { get; set; }
        public int? agentUId { get; set; }
        public string assignStatus { get; set; }
    }
}
