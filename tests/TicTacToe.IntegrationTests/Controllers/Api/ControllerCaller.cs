using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using TicTacToe.Domain.Core;
using System.Net;

namespace TicTacToe.IntegrationTests.Controllers.Api
{
    public class ControllerCaller
    {
        private readonly HttpClient _client;
        public ControllerCaller(HttpClient client)
        {
            _client = client;
        }
        public Result<T> Post<T>(string url, IDictionary<string, string> parameters, object viewModelToSend)
        {
            var uriWithParameters = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(url, parameters);

            return Post<T>(uriWithParameters, viewModelToSend);
        }

        public Result<T> Post<T>(string url, object viewModelToSend)
        {
            var content = new StringContent(JsonConvert.SerializeObject(viewModelToSend), Encoding.UTF8, "application/json");

            var response = _client.PostAsync(url, content).Result;
            var responseContentTask = response.Content.ReadAsStringAsync();
            responseContentTask.Wait();

            if(response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Result.Ok(JsonConvert.DeserializeObject<T>(responseContentTask.Result));
            }
            else
            {
                return Result.Fail<T>(JsonConvert.DeserializeObject<string>(responseContentTask.Result));
            }
            
        }
        
        public Result<T> Get<T>(string uri, IDictionary<string, string> parameters)
        {
            var uriWithParameters = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(uri, parameters);

            return Get<T>(uriWithParameters);
        }
        public Result<T> Get<T>(string uri)
        {
            var response = _client.GetAsync(uri).Result;
            var responseContentTask = response.Content.ReadAsStringAsync();
            responseContentTask.Wait();

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Result.Ok(JsonConvert.DeserializeObject<T>(responseContentTask.Result));
            }
            else
            {
                return Result.Fail<T>(JsonConvert.DeserializeObject<string>(responseContentTask.Result));
            }
            
        }
    }
}
