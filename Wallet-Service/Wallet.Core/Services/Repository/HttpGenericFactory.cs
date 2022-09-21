using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Dtos;
using Wallet.Model.Enums;

namespace Wallet.Core.Repository
{
     public class HttpGenericFactory : IHttpGenericFactory
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HttpGenericFactory> _logger;
        public HttpGenericFactory(IHttpClientFactory clientFactory, ILogger<HttpGenericFactory> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<Tuple<bool, string>> Post(string sendData, string baseUrl,
           string endPoint, List<CustomHeader> CustomHeader, ApiHttpVerbs SwerveHttpVerbs, bool isBasicAuth = false, string basicAuthCredentials = "",
           string token = "", bool hasCustomHeader = false)
        {
            HttpResponseMessage response = null;
            StringContent httpContent = null;

            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    if (isBasicAuth)
                    {
                        var byteArray = Encoding.ASCII.GetBytes(basicAuthCredentials);
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    }
                    else
                    {
                        client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);
                    }

                    if (hasCustomHeader)
                    {
                        foreach (var customHeader in CustomHeader)
                        {
                            client.DefaultRequestHeaders.Add(customHeader.Name, customHeader.Value);
                        }
                    }

                    httpContent = new StringContent(sendData, Encoding.UTF8, "application/json");

                    switch (SwerveHttpVerbs)
                    {
                        case ApiHttpVerbs.Post:
                            response = await client.PostAsync(endPoint, httpContent);
                            break;
                        case ApiHttpVerbs.Put:
                            response = await client.PutAsync(endPoint, httpContent);
                            break;
                        default:
                            response = await client.PostAsync(endPoint, httpContent);
                            break;
                    }

                    string result = await response.Content.ReadAsStringAsync();

                    return Tuple.Create(response.IsSuccessStatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            finally
            {
                response.Dispose();
                httpContent.Dispose();
            }

        }


        public async Task<Tuple<bool, string>> Post(HttpPostOrPutDto httpPostOrPutDto, string token = "", string basicAuthCredentials = "")
        {
            HttpResponseMessage response = null;
            StringContent httpContent = null;

            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    client.BaseAddress = new Uri(httpPostOrPutDto.BaseUrl);

                    string endPoint = httpPostOrPutDto.EndPoint;
                    var CustomHeader = httpPostOrPutDto.CustomHeaders;

                    if (!string.IsNullOrWhiteSpace(basicAuthCredentials))
                    {
                        var byteArray = Encoding.ASCII.GetBytes(basicAuthCredentials);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    }
                    else if (!string.IsNullOrWhiteSpace(token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    if (CustomHeader != null)
                        foreach (var customHeader in CustomHeader)
                        {
                            if (customHeader.Name != "Content-Type")
                                client.DefaultRequestHeaders.Add(customHeader.Name, customHeader.Value);
                            else
                                client.DefaultRequestHeaders.TryAddWithoutValidation(customHeader.Name, customHeader.Value);

                        }

                    httpContent = new StringContent(httpPostOrPutDto.SendData, Encoding.UTF8, "application/json");
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                    switch (httpPostOrPutDto.ApiHttpVerbs)
                    {
                        case ApiHttpVerbs.Post:
                            response = await client.PostAsync(endPoint, httpContent);
                            break;
                        case ApiHttpVerbs.Put:
                            response = await client.PutAsync(endPoint, httpContent);
                            break;
                    }

                    string result = await response.Content.ReadAsStringAsync();

                    return Tuple.Create(response.IsSuccessStatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            finally
            {
                response.Dispose();
                httpContent.Dispose();
            }
        }

        public async Task<Tuple<bool, string>> Get(HttpGetOrDelete httpGetOrDelete, string basicAuthCredentials = "", string token = "")
        {
            HttpResponseMessage response = null;

            try
            {
                string requestUri = httpGetOrDelete.EndPoint + httpGetOrDelete.QueryString;
                var customeHeaders = httpGetOrDelete.CustomHeaders;

                using (var client = _clientFactory.CreateClient())
                using (var request =
                    new HttpRequestMessage((httpGetOrDelete.ApiHttpVerbs == ApiHttpVerbs.Get) ? HttpMethod.Get : HttpMethod.Delete, requestUri))
                {
                    client.BaseAddress = new Uri(httpGetOrDelete.BaseUrl);

                    if (!string.IsNullOrWhiteSpace(basicAuthCredentials))
                    {
                        var byteArray = Encoding.ASCII.GetBytes(basicAuthCredentials);
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    }
                    else if (!string.IsNullOrWhiteSpace(token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    if (customeHeaders != null)
                        foreach (var customHeader in customeHeaders)
                            client.DefaultRequestHeaders.Add(customHeader.Name, customHeader.Value);

                    switch (httpGetOrDelete.ApiHttpVerbs)
                    {
                        case ApiHttpVerbs.Get:
                            response = await client.SendAsync(request);
                            break;
                        case ApiHttpVerbs.Delete:
                            response = await client.DeleteAsync(requestUri);
                            break;
                    }

                    var result = await response.Content.ReadAsStringAsync();

                    return Tuple.Create(response.IsSuccessStatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            finally
            {
                response.Dispose();
            }

        }

        public async Task<Tuple<bool, string>> Post(Dictionary<string, string> sendData, string baseUrl,
      string endPoint, bool isBasicAuth = false, string basicAuthCredentials = "", string token = "")
        {
            HttpResponseMessage response = null;

            try
            {
                using (var client = _clientFactory.CreateClient())
                using (var httpContent = new FormUrlEncodedContent(sendData))
                {
                    client.BaseAddress = new Uri(baseUrl);

                    if (isBasicAuth)
                    {
                        var byteArray = Encoding.ASCII.GetBytes(basicAuthCredentials);
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    }
                    else
                    {
                        client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("JWT", token);
                    }

                    response = await client.PostAsync(endPoint, httpContent);

                    var result = await response.Content.ReadAsStringAsync();

                    return Tuple.Create(response.IsSuccessStatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            finally
            {
                response.Dispose();
            }

        }

   
    }
}
