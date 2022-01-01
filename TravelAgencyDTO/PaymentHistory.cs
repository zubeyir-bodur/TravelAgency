using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class PaymentHistory
    {
        public int uId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime reserveStartDate { get; set; }
        public DateTime reserveEndDate { get; set; }
        public string tourName { get; set; }
        public decimal price { get; set; }
        public bool isBooked { get; set; }
        public int numReserving { get; set; }
        public decimal extraActivitiesPer { get; set; }
        public decimal totalPaymentDone { get; set; }
    }
}
