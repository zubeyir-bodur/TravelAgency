using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDTO
{
    public class RegisterInfo
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }

        public string phoneNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime birthDate { get; set; }
        public string type { get; set; }
    }
}
