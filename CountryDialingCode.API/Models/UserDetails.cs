using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDialingCode.API.Models
{
    public class UserDetails
    {
        public UserDetails()
        {
            Name = "";

            Languages = new List<string>();
        }

        public string Name { get; set; }

        public List<string> Languages { get; set; }
    }
}
