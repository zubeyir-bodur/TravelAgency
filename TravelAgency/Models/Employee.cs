using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Employee
    {
        public int UId { get; set; }
        public decimal? Salary { get; set; }

        public virtual Users U { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Guide Guide { get; set; }
    }
}
