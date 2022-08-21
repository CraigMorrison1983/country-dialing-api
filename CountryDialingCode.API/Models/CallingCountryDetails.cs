using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDialingCode.API.Models
{
    public class CallingCountryDetails
    {
        public string DefaultLanguage { get; set; }

        public string Name { get; set; }

        public string UserDefaultCountryName { get; set; }

        public string Region { get; set; }

        public string FlagUrl { get; set; }
    }
}
