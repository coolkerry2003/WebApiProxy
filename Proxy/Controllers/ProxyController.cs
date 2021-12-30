using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Proxy.Controllers
{
    public class ProxyController : ApiController
    {
        string baseUrl = "http://127.0.0.1:8080";
        [Route("proxy/{*url}")]
        public async Task<HttpResponseMessage> Get(string url)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{baseUrl}/{url}/{Request.RequestUri.Query}";
                var proxyRequest = new HttpRequestMessage(Request.Method, requestUrl);
                foreach (var header in Request.Headers)
                {
                    proxyRequest.Headers.Add(header.Key, header.Value);
                }
                return await httpClient.SendAsync(proxyRequest, HttpCompletionOption.ResponseContentRead);
            }
        }
    }
}