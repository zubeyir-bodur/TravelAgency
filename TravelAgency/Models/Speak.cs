using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Speak
    {
        public int UId { get; set; }
        public string LanguageName { get; set; }

        public virtual Languages LanguageNameNavigation { get; set; }
        public virtual Guide U { get; set; }
    }
}
