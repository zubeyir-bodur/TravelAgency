using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Festival
    {
        public int ActivityId { get; set; }
        public string FoodCatalog { get; set; }
        public int? AgeLimit { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
