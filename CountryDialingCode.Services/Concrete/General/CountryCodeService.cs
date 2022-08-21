using CountryDialingCode.Core.Constants;
using CountryDialingCode.Core.Entities;
using CountryDialingCode.Services.Abstract.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryDialingCode.Services.Concrete.General
{
    public class CountryCodeService : ICountryCodeService
    {
        private readonly IHttpApiConsumer _httpApiConsumer;

        public CountryCodeService(IHttpApiConsumer httpApiConsumer)
        {
            _httpApiConsumer = httpApiConsumer ??
                throw new ArgumentNullException(nameof(httpApiConsumer));
        }

        public async Task<Country> GetCountryDetailsAsync(string callingCode)
        {
            string jsonResult = "";

            if (callingCode.StartsWith("1"))
            {
                // Northern American territories. API does not find records if we pass the whole calling code so we just need to pass 1
                // This returns a list that we then need to handle
                jsonResult = await _httpApiConsumer.GetAsync(RestCountriesApiConstants.CallingCodeEndPoint, "1");
            }

            else
            {
                jsonResult = await _httpApiConsumer.GetAsync(RestCountriesApiConstants.CallingCodeEndPoint, callingCode);

            }

            var countries = JsonConvert.DeserializeObject<List<Country>>(jsonResult);

            var selectedCountry = new Country();

            if (countries.Count == 1)
            {
                selectedCountry = countries[0];
            }

            if (countries.Count > 1)
            {
                var json2 = await _httpApiConsumer.GetAsync(RestCountriesApiConstants.CountriesListEndPoint, "");
                var countryList = JsonConvert.DeserializeObject<List<FullCountry>>(json2);

                var country = countryList.Where(w => callingCode == w.DialingCodes.Suffixes.Where(w1 => w1).S
            }

            return selectedCountry;
        }
    }
}
