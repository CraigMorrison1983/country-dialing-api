using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDialingCode.API.Models
{
    public class PhoneCallDetails
    {
        public PhoneCallDetails()
        {
            User = new UserDetails();
            CallingCountry = new CallingCountryDetails();
        }

        public string PhoneNumber { get; set; }

        public string CallingCode { get; set; }

        public string CountryCode { get; set; }

        public UserDetails User { get; set; }

        public CallingCountryDetails CallingCountry { get; set; }
    }
}
