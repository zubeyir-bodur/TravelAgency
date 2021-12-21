using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
            Review = new HashSet<Review>();
        }

        public int UId { get; set; }
        public string CAddress { get; set; }
        public decimal? Wallet { get; set; }

        public virtual Users U { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
