using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class TotalPaymentDTO
    {
        public int uId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal totalPaymentSoFar { get; set; }
    }
}
