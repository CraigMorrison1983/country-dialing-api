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
            string countryJsonResult = "";

            if (callingCode.StartsWith("1"))
            {
                // Northern American territories. API does not find records if we pass the whole calling code so we just need to pass 1
                // This returns a list that we then need to handle
                countryJsonResult = await _httpApiConsumer.GetAsync(RestCountriesApiConstants.CallingCodeEndPoint, "1");
            }
            else
            {
                countryJsonResult = await _httpApiConsumer.GetAsync(RestCountriesApiConstants.CallingCodeEndPoint, callingCode);
            }

            var countries = JsonConvert.DeserializeObject<List<Country>>(countryJsonResult);

            // No country matched the supplied calling code
            if (countries == null)
            {
                return null;
            }

            var selectedCountry = new Country();

            if (countries.Count == 1)
            {
                selectedCountry = countries[0];
            }

            if (countries.Count > 1)
            {
                // More than 1 country matched the calling code so we need to check the full details that includes suffixes.
                // Likely to be North America but there are others such as UK
                var fullCountryListJson = await _httpApiConsumer.GetAsync(RestCountriesApiConstants.CountriesListEndPoint, "");

                var countryList = JsonConvert.DeserializeObject<List<FullCountry>>(fullCountryListJson);
                string selectedCountryCode = "";

                if (countryList.Where(w => w.CombinedDialingCodes.Contains(callingCode)).Select(S => S.CountryCode).Any())
                {
                    if (countryList.Where(w => w.CombinedDialingCodes.Contains(callingCode)).Select(S => S.CountryCode).Count() > 1)
                    {
                        // Still more than 1 country matching.
                        // Some edge cases like the UK that shares calling code with the surrounding isles. For this we'll select the first one that is "independent"
                        selectedCountryCode = countryList.Where(w => w.CombinedDialingCodes.Contains(callingCode) && w.IsIndependent == true).Select(S => S.CountryCode).FirstOrDefault();
                    }
                    else
                    {
                        selectedCountryCode = countryList.Where(w => w.CombinedDialingCodes.Contains(callingCode)).Select(S => S.CountryCode).FirstOrDefault();
                    }
                }
                else
                {
                    // No countries matching supplied country code
                    return null;
                }


                selectedCountry = countries.Where(w => w.CountryCode == selectedCountryCode).SingleOrDefault();
            }

            return selectedCountry;
        }
    }
}
