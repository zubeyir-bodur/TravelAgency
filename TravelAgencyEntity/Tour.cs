using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Tour
    {
        public Tour()
        {
            Activities = new HashSet<Activity>();
            GuidesFeedbacks = new HashSet<GuidesFeedback>();
            TourReservations = new HashSet<TourReservation>();
            TourReviews = new HashSet<TourReview>();
            Visits = new HashSet<Visit>();
        }

        public int TourId { get; set; }
        public string City { get; set; }
        public string TourName { get; set; }
        public DateTime? TourStartDate { get; set; }
        public string TourDescription { get; set; }
        public decimal? Price { get; set; }
        public int? DiscountId { get; set; }
        public DateTime? TourEndDate { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual AssignGuide AssignGuide { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<GuidesFeedback> GuidesFeedbacks { get; set; }
        public virtual ICollection<TourReservation> TourReservations { get; set; }
        public virtual ICollection<TourReview> TourReviews { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
