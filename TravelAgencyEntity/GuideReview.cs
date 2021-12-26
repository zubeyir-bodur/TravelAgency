using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class GuideReview
    {
        public int ReviewId { get; set; }
        public int UId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Guide UIdNavigation { get; set; }
    }
}
