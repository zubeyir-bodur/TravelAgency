using System;
using System.Collections.Generic;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class Speak
    {
        public int UId { get; set; }
        public string LanguageName { get; set; }

        public virtual Language LanguageNameNavigation { get; set; }
        public virtual Guide UIdNavigation { get; set; }
    }
}
