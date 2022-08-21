using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
    public class Country
    {
        public Country()
        {
            Languages = new List<Language>();
            Translations = new Dictionary<string, string>();
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "alpha2Code")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "flag")]
        public string FlagUrl { get; set; }

        [JsonProperty(PropertyName = "translations")]
        public IDictionary<string, string> Translations { get; set; }
    }
}
