using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
    public class DialingRoute
    {
        public DialingRoute()
        {
            Suffixes = new List<string>();
        }

        [JsonProperty(PropertyName = "root")]
        public string Root { get; set; }

        [JsonProperty(PropertyName = "suffixes")]
        public List<string> Suffixes { get; set; }
    }
}
