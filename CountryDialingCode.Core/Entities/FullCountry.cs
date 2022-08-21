using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
    public class FullCountry
    {
        [JsonProperty(PropertyName = "name")]
        public CountryName CountryDetails { get; set; }

        [JsonProperty(PropertyName = "cca2")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "idd")]
        public DialingRoute DialingCodes { get; set; }

        [JsonIgnore]
        public List<string> CombinedDialingCodes
        {
            get
            {
                var list = new List<string>();
                foreach (var item in DialingCodes.Suffixes)
                {
                    list.Add($"{DialingCodes.Root.Replace("+", "")}{item}");
                }

                return list;
            }
        }
    }
}
