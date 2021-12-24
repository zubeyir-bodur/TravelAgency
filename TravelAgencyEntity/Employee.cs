using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Employee
    {
        public int UId { get; set; }
        public decimal? Salary { get; set; }

        public virtual User UIdNavigation { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Guide Guide { get; set; }
    }
}
