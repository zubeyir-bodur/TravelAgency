using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Languages
    {
        public Languages()
        {
            Speak = new HashSet<Speak>();
        }

        public string LanguageName { get; set; }

        public virtual ICollection<Speak> Speak { get; set; }
    }
}
