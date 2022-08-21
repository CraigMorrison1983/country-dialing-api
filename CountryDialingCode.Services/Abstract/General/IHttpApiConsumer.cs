using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountryDialingCode.Services.Abstract.General
{
    public interface IHttpApiConsumer
    {
       Task<string> GetAsync(string endPoint, string parameterList);
    }
}
