using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Utilties.Requests;

namespace Wallet.Core.Interfaces
{
    public interface IHttpService
    {
        Task<T> SendPostRequest<T, U>(JsonContentPostRequest<U> request);
        Task<T> SendGetRequest<T>(GetRequest request);
    }
}
