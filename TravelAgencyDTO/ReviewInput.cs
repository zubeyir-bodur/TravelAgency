using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class ReviewInput
    {
        public int id { get; set; }
        public int stars { get; set; }
        public string comment { get; set; }
        public DateTime entryDate { get; set; }
        public int uId { get; set; }
    }
}
