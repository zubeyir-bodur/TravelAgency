using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Reservation
    {
        public int ReserveId { get; set; }
        public DateTime? ReserveStartDate { get; set; }
        public int? NumReserving { get; set; }
        public bool? IsBooked { get; set; }
        public int? UId { get; set; }
        public DateTime? ReserveEndDate { get; set; }

        public virtual Customer UIdNavigation { get; set; }
        public virtual HotelReservation HotelReservation { get; set; }
        public virtual TourReservation TourReservation { get; set; }
    }
}
