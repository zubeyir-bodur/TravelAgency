using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Tour
    {
        public Tour()
        {
            Activity = new HashSet<Activity>();
            GuidesFeedback = new HashSet<GuidesFeedback>();
            TourReservation = new HashSet<TourReservation>();
            TourReview = new HashSet<TourReview>();
            Visits = new HashSet<Visits>();
        }

        public int TourId { get; set; }
        public string City { get; set; }
        public string TourName { get; set; }
        public DateTime? TourStartDate { get; set; }
        public int? TourDays { get; set; }
        public string TourDescription { get; set; }
        public decimal? Price { get; set; }
        public int? DiscountId { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual AssignGuide AssignGuide { get; set; }
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<GuidesFeedback> GuidesFeedback { get; set; }
        public virtual ICollection<TourReservation> TourReservation { get; set; }
        public virtual ICollection<TourReview> TourReview { get; set; }
        public virtual ICollection<Visits> Visits { get; set; }
    }
}
