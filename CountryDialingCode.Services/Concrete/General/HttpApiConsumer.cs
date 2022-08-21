using CountryDialingCode.Core.Constants;
using CountryDialingCode.Services.Abstract.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CountryDialingCode.Services.Concrete.General
{
    public class HttpApiConsumer : IHttpApiConsumer
    {
        private readonly HttpClient _httpClient;

        public HttpApiConsumer()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(RestCountriesApiConstants.BaseApiURL);
        }

        public async Task<string> GetAsync(string endPoint, string parameterList)
        {
            string jsonResult = "";

            var responseTask = await _httpClient.GetAsync($"{endPoint}{parameterList}").ConfigureAwait(false);

            var resultTask = Task.FromResult(responseTask);

            var result = resultTask.Result;
            if (result.IsSuccessStatusCode)
            {
                jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            }

            return jsonResult;
        }
    }
}
