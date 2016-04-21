using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TbaApiClient.Cache;
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

        public static HttpClient GetHttpClientWithCaching(DateTimeOffset cachedate)
        {
            HttpClient httpClient = GetHttpClientWithCaching();
            if (cachedate != DateTimeOffset.MinValue)
            {
                httpClient.DefaultRequestHeaders.IfModifiedSince = cachedate;
            }

            return httpClient;
        }

        /// <summary>
        /// Gets the response data from a URI. If 1) we have a cache, 2) the URI content is in the cache, and 3) the URI content is not newer than the cache,
        /// we return the cache data.
        /// </summary>
        /// <param name="uri">the URI</param>
        /// <param name="cache">the cache object</param>
        /// <param name="cachekey">the key for the cache content related to the uri</param>
        /// <returns></returns>
        public async static Task<string> GetResponseFromUriOrCache(Uri uri, FileCache cache, string cachekey)
        {
            string responseData;

            try
            {
                if (cache == null) // no cache - just get the data from the URI
                {
                    using (var httpClient = GetHttpClientWithCaching())
                    {
                        using (var response = await httpClient.GetAsync(uri))
                        {
                            responseData = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                else // we have a cache object, so we need to pull that unless the URI content is newer
                {
                    var cachevalue = await cache.TryGetCacheItem(cachekey);
                    var cachedate = await cache.GetCacheDate(cachekey);

                    using (var httpClient = GetHttpClientWithCaching(cachedate)) // passing the cachedate sets the "IfModifiedSince" header
                    {
                        using (var response = await httpClient.GetAsync(uri))
                        {
                            if (response.StatusCode == HttpStatusCode.NotModified)
                            {
                                responseData = cachevalue;
                            }
                            else
                            {
                                responseData = await response.Content.ReadAsStringAsync();
                                await cache.StoreCache(cachekey, responseData);
                            }
                        }
                    }
                }

                return responseData;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
