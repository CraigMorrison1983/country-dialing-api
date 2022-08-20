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
        private readonly IHttpApiConsumer _httpApiConsumer;
        private readonly IPhoneUserService _phoneUserService;

        public PhoneDialingController(IHttpApiConsumer httpApiConsumer, IPhoneUserService phoneUserService)
        {
            _httpApiConsumer = httpApiConsumer ??
                throw new ArgumentNullException(nameof(httpApiConsumer));

            _phoneUserService = phoneUserService ??
                throw new ArgumentNullException(nameof(phoneUserService));
        }

        [HttpGet()]
        public async Task<ActionResult> GetInfoAsync(string phoneNumber)
        {

            List<Country> countries = await _httpApiConsumer.Get<List<Country>>("callingcode/", "44");


            var users = _phoneUserService.GetPhoneUsers();

            var users1 = _phoneUserService.GetPhoneUsersByLanguage("khj");

            return Ok();
        }
    }
}
