using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
    public class FullCountry
    {
        public FullCountry()
        {
            CountryDetails = new CountryNameInformation();
            DialingCodes = new DialingRoute();
        }

        [JsonProperty(PropertyName = "name")]
        public CountryNameInformation CountryDetails { get; set; }

        [JsonProperty(PropertyName = "cca2")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "idd")]
        public DialingRoute DialingCodes { get; set; }

        [JsonProperty(PropertyName = "independent")]
        public bool IsIndependent { get; set; }

        [JsonIgnore]
        public List<string> CombinedDialingCodes
        {
            get
            {
                var list = new List<string>();
                foreach (var item in DialingCodes.Suffixes)
                {
                    list.Add($"{DialingCodes.Root.Replace("+", "")}{item.Trim()}");
                }

                return list;
            }
        }
    }
}
