using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? Rating { get; set; }
        public int UId { get; set; }

        public virtual Customer UIdNavigation { get; set; }
        public virtual GuideReview GuideReview { get; set; }
        public virtual TourReview TourReview { get; set; }
    }
}
