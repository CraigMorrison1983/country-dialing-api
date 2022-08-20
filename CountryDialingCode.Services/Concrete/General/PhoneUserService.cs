using CountryDialingCode.Core.Entities;
using CountryDialingCode.Services.Abstract.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountryDialingCode.Services.Concrete.General
{
    public class PhoneUserService : IPhoneUserService
    {
        private readonly List<PhoneUser> _phoneUsers;

        public PhoneUserService()
        {
            _phoneUsers = new List<PhoneUser>();

            _phoneUsers.Add(new PhoneUser { Name = "01", Languages = new List<string>() { "en" } });
            _phoneUsers.Add(new PhoneUser { Name = "02", Languages = new List<string>() { "en" } });
            _phoneUsers.Add(new PhoneUser { Name = "03", Languages = new List<string>() { "en", "fr", "it", "de" } });
            _phoneUsers.Add(new PhoneUser { Name = "04", Languages = new List<string>() { "pl", "es", "sv", "ru" } });
            _phoneUsers.Add(new PhoneUser { Name = "05", Languages = new List<string>() { "en", "ra" } });
            _phoneUsers.Add(new PhoneUser { Name = "06", Languages = new List<string>() { "ja", "zu", "ta", "af", "sq", "en" } });
            _phoneUsers.Add(new PhoneUser { Name = "07", Languages = new List<string>() { "ca", "zh", "ko", "ms" } });
            _phoneUsers.Add(new PhoneUser { Name = "08", Languages = new List<string>() { "fr", "dk" } });
            _phoneUsers.Add(new PhoneUser { Name = "09", Languages = new List<string>() { "fa", "fi", "lv", "en" } });
            _phoneUsers.Add(new PhoneUser { Name = "10", Languages = new List<string>() { "en", "de", "fr", "da", "nl" } });
        }

        public List<PhoneUser> GetPhoneUsers()
        {
            return _phoneUsers;
        }

        public List<PhoneUser> GetPhoneUsersByLanguage(string language)
        {
            return _phoneUsers.Where(w => w.Languages.Contains(language)).ToList();
        }
    }
}
