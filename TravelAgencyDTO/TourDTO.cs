using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class TourDTO
    {
        public int tourId { get; set; }
        public string city { get; set; }
        public string tourName { get; set; }
        public DateTime? tourStartDate { get; set; }
        public DateTime? tourEndDate { get; set; }
        public string tourDescription { get; set; }
        public decimal price { get; set; }
        public int discountPercents { get; set; }
    }
}
