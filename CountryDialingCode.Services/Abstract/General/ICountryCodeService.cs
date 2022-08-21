using CountryDialingCode.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountryDialingCode.Services.Abstract.General
{
    public interface ICountryCodeService
    {
        Task<Country> GetCountryDetailsAsync(string callingCode);
    }
}
