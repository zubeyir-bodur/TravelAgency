using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? Rating { get; set; }
        public int UId { get; set; }

        public virtual Customer U { get; set; }
        public virtual GuideReview GuideReview { get; set; }
        public virtual TourReview TourReview { get; set; }
    }
}
