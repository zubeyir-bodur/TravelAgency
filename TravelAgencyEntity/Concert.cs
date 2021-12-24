using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Concert
    {
        public int ActivityId { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
