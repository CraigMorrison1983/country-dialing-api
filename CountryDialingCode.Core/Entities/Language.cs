using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Entities
{
    public class Language
    {
        public Language()
        {
            LanguageCode = "";
        }

        [JsonProperty(PropertyName = "iso639_1")]
        public string LanguageCode { get; set; }
    }
}
