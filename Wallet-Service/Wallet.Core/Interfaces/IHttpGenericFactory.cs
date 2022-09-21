using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos;

namespace Wallet.Core.Interfaces
{
     public interface IHttpGenericFactory
    {
        Task<Tuple<bool, string>> Get(HttpGetOrDelete httpGetOrDelete, string basicAuthCredentials = "", string token = "");

        Task<Tuple<bool, string>> Post(Dictionary<string, string> sendData, string baseUrl,
           string endPoint, bool isBasicAuth = false, string basicAuthCredentials = "", string token = "");

        Task<Tuple<bool, string>> Post(HttpPostOrPutDto HttpPostOrPutDto, string token = "", string basicAuthCredentials = "");
    }
}
