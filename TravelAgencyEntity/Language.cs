using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Language
    {
        public Language()
        {
            Speaks = new HashSet<Speak>();
        }

        public string LanguageName { get; set; }

        public virtual ICollection<Speak> Speaks { get; set; }
    }
}
