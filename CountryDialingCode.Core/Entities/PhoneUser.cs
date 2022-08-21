using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
   public class PhoneUser
    {
        public PhoneUser()
        {
            Name = "";
            Languages = new List<string>();
        }

        public string Name { get; set; }

        public List<string> Languages { get; set; }
    }
}
