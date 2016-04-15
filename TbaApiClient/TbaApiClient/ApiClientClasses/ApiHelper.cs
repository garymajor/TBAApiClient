using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace TbaApiClient
{
    public static class ApiHelper
    {
        /// <summary>
        /// Setup an HttpClient object with Cache Read/Write set.
        /// </summary>
        /// <returns>HttpClient</returns>
        public static HttpClient GetHttpClientWithCaching()
        {
            // set caching
            var httpProtocolFilter = new HttpBaseProtocolFilter();
            httpProtocolFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            httpProtocolFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.Default;

            // create the client with the caching filters
            HttpClient httpClient = new HttpClient(httpProtocolFilter);

            // set the request headers
            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

            return httpClient;
        }
    }
}
