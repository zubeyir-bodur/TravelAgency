using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Festival
    {
        public int ActivityId { get; set; }
        public string FoodCatalog { get; set; }
        public int? AgeLimit { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
