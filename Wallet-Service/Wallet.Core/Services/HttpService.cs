using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Utilties.Requests;

namespace Wallet.Core.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClient;

        public HttpService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
     
        public async Task<T> SendPostRequest<T, U>(JsonContentPostRequest<U> request)
        {
            var client = _httpClient.CreateClient();

            HttpRequestMessage message = new HttpRequestMessage();

            message.RequestUri = new Uri(request.Url);
            message.Method = HttpMethod.Post;

            message.Headers.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Clear();

            if (!string.IsNullOrEmpty(request.AccessToken))
            {
                message.Headers.Add("Authorization", $"Bearer {request.AccessToken}");
            }
            var t = JsonConvert.SerializeObject(request.Data);
            message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8,
                "application/json");


            HttpResponseMessage response = await client.SendAsync(message);
            var responseContent = await response.Content.ReadAsStringAsync();
            var linkResponse = JsonConvert.DeserializeObject<T>(responseContent);

            return linkResponse;

        }

        public async Task<T> SendGetRequest<T>(GetRequest request)
        {
            var client = _httpClient.CreateClient();

            HttpRequestMessage message = new HttpRequestMessage();

            message.RequestUri = new Uri(request.Url);
            message.Method = HttpMethod.Get;
            message.Headers.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Clear();

            if (!string.IsNullOrEmpty(request.AccessToken))
            {
                message.Headers.Add("Authorization", $"Bearer {request.AccessToken}");
            }

            HttpResponseMessage response = await client.SendAsync(message);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            var linkResponse = JsonConvert.DeserializeObject<T>(responseContent);

            return linkResponse;

        }

        
    }
}
