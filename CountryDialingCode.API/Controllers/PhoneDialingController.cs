using CountryDialingCode.API.Models;
using CountryDialingCode.Core.Constants;
using CountryDialingCode.Core.Entities;
using CountryDialingCode.Core.Utils;
using CountryDialingCode.Services.Abstract.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDialingCode.API.Controllers
{
    [ApiController]
    [Route("api/PhoneDialing")]
    public class PhoneDialingController : ControllerBase
    {
        private readonly ICountryCodeService _httpApiConsumer;
        private readonly IPhoneUserService _phoneUserService;

        public PhoneDialingController(ICountryCodeService httpApiConsumer, IPhoneUserService phoneUserService)
        {
            _httpApiConsumer = httpApiConsumer ??
                throw new ArgumentNullException(nameof(httpApiConsumer));

            _phoneUserService = phoneUserService ??
                throw new ArgumentNullException(nameof(phoneUserService));
        }

        [HttpGet("{phoneNumber}", Name = "GetCallingCountryInfo")]
        public async Task<ActionResult> GetCallingCountryInfoAsync(string phoneNumber)
        {

            if (phoneNumber.Replace(" ", "").Replace("+", "").Trim().Length < 11)
            {
                return BadRequest(new { Status = 400, Title = "Bad Request", Message = "Phone number should be minimum 11 characters." });
            }


            string callingCode = PhoneNumberUtils.GetCallingCodeFromPhoneNumber(phoneNumber);

            if (callingCode.Trim().Length > 4)
            {
                return BadRequest(new { Status = 400, Title = "Bad Request", Message = "Calling code should not be more than 4 characters." });
            }

            Country country = await _httpApiConsumer.GetCountryDetailsAsync(callingCode);

            List<PhoneCallDetails> phoneCalls = new List<PhoneCallDetails>();

            if (country != null)
            {
                var userList = _phoneUserService.GetPhoneUsersByLanguage(country.Languages[0].LanguageCode);

                PhoneUser selectedUser = new PhoneUser();


                if (userList.Count == 0)
                {
                    selectedUser = _phoneUserService.GetMostLingualPhoneUser();
                }
                else
                {
                    // Select a user
                    selectedUser = _phoneUserService.SelectRandomPhoneUser(userList);
                }


                PhoneCallDetails phoneCallDetails = new PhoneCallDetails();

                phoneCallDetails.PhoneNumber = phoneNumber;
                phoneCallDetails.CallingCode = callingCode;
                phoneCallDetails.CountryCode = country.CountryCode;
                phoneCallDetails.User = new UserDetails() { Name = selectedUser.Name, Languages = selectedUser.Languages };
                phoneCallDetails.CallingCountry.DefaultLanguage = country.Languages[0].LanguageCode;
                phoneCallDetails.CallingCountry.Name = country.Name;
                phoneCallDetails.CallingCountry.UserDefaultCountryName = GetCountryNameInUserDefaultLanguage(selectedUser.Languages[0], country.Translations) ?? country.Name;
                phoneCallDetails.CallingCountry.Region = country.Region;
                phoneCallDetails.CallingCountry.FlagUrl = country.FlagUrl;

                return Ok(phoneCallDetails);
            }
            else
            {
                return NotFound(new { Status = 404, Title = "Not Found", Message = $"Calling code {callingCode} is not valid. Cannot find matching country." });
            }

        }

        private string GetCountryNameInUserDefaultLanguage(string userDefaultLanguage, IDictionary<string, string> translations)
        {
            return translations.Where(w => w.Key == userDefaultLanguage).Select(s => s.Value).SingleOrDefault();
        }

    }
}
