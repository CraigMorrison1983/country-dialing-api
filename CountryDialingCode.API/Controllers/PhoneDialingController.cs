using CountryDialingCode.API.Models;
using CountryDialingCode.Core.Constants;
using CountryDialingCode.Core.Entities;
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

        [HttpGet()]
        public async Task<ActionResult> GetInfoAsync(string phoneNumber)
        {

            if (phoneNumber.Replace(" ", "").Replace("+", "").Trim().Length < 11)
            {
                return BadRequest(new { Status = 400, Title = "Bad Request", Message = "Phone number should be minimum 11 characters." });
            }


            string callingCode = GetCallingCodeFromPhoneNumber(phoneNumber);

            Country country = await _httpApiConsumer.GetCountryDetailsAsync(callingCode);
            List<PhoneCallDetails> phoneCalls = new List<PhoneCallDetails>();


            if (country != null)
            {
                var userList = _phoneUserService.GetPhoneUsersByLanguage(country.Languages[0].LanguageCode);

                foreach (PhoneUser user in userList)
                {
                    PhoneCallDetails phoneCallDetails = new PhoneCallDetails();

                    phoneCallDetails.PhoneNumber = phoneNumber;
                    phoneCallDetails.CallingCode = callingCode;
                    phoneCallDetails.CountryCode = country.CountryCode;
                    phoneCallDetails.User = new UserDetails() { Name = user.Name, Languages = user.Languages };
                    phoneCallDetails.CallingCountry.DefaultLanguage = country.Languages[0].LanguageCode;
                    phoneCallDetails.CallingCountry.Name = country.Name;
                    phoneCallDetails.CallingCountry.UserDefaultCountryName = GetCountryNameInUserDefaultLanguage(user.Languages[0], country.Translations) ?? country.Name;
                    phoneCallDetails.CallingCountry.Region = country.Region;
                    phoneCallDetails.CallingCountry.FlagUrl = country.FlagUrl;

                    phoneCalls.Add(phoneCallDetails);
                }
            }

            return Ok(phoneCalls);
        }

        private string GetCallingCodeFromPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", "");

            phoneNumber = phoneNumber.Replace("+", "");


            return phoneNumber.Remove(phoneNumber.Length - 10, 10);

        }

        private string GetCountryNameInUserDefaultLanguage(string userDefaultLanguage, IDictionary<string, string> translations)
        {
            return translations.Where(w => w.Key == userDefaultLanguage).Select(s => s.Value).SingleOrDefault();
        }
    }
}
