using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
    public class CountryNameInformation
    {
        public CountryNameInformation()
        {
            Common = "";
            Official = "";
        }

        [JsonProperty(PropertyName = "common")]
        public string Common { get; set; }

        [JsonProperty(PropertyName = "official")]
        public string Official { get; set; }
    }
}
