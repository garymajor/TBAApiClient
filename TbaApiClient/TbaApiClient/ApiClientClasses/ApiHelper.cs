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
        /// Setup an HttpClient object with the app request headers set.
        /// </summary>
        /// <returns>HttpClient</returns>
        public static HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();

            // set the request headers
            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

            return httpClient;
        }

        /// <summary>
        /// Setup an HttpClient object with the app request headers set and the If-Modified-Since Header set to the cache date value.
        /// </summary>
        /// <param name="cachedate">The Last Updated date to use as the If-Modified-Since date in the request header</param>
        /// <returns>HttpClient</returns>
        public static HttpClient GetHttpClient(DateTimeOffset cachedate)
        {
            HttpClient httpClient = GetHttpClient();
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
            string cachedatekey = cachekey + "-LastUpdated";

            try
            {
                if (cache == null) // no cache - just get the data from the URI
                {
                    using (var httpClient = GetHttpClient())
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
                    var cachedatevalue = await cache.TryGetCacheItem(cachedatekey);

                    // convert the cache date value to a DateTimeOffset
                    DateTimeOffset cachedate;
                    if (string.IsNullOrEmpty(cachedatevalue))
                    {
                        cachedate = DateTimeOffset.MinValue; // couldn't read the cache date, so invalidating it by setting it to the Min value.
                    }
                    else
                    {
                        cachedate = new DateTimeOffset(Convert.ToDateTime(cachedatevalue));
                    }

                    using (var httpClient = GetHttpClient(cachedate)) // passing the cachedate sets the "IfModifiedSince" header
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
                                await cache.StoreCache(cachedatekey, response.Content.Headers.LastModified.ToString());
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
