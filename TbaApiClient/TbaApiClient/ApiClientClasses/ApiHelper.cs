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

            return new HttpClient(httpProtocolFilter);
        }
    }
}
