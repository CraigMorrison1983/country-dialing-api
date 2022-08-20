using CountryDialingCode.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Services.Abstract.General
{
    public interface IPhoneUserService
    {
        List<PhoneUser> GetPhoneUsers();

        List<PhoneUser> GetPhoneUsersByLanguage(string language);
    }
}
