using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class TourFilter
    {
        public string city { get; set; }
        public DateTime dateRangeStart { get; set; }
        public DateTime dateRangeEnd { get; set; }
        public decimal priceRangeStart { get; set; }
        public decimal priceRangeEnd { get; set; }
        public int numPeople { get; set; }
    }
}
