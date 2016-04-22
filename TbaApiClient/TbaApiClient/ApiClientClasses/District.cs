using Newtonsoft.Json;
using TbaApiClient.Cache;
using TbaApiClient.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TbaApiClient
{
    public class District
    {
        public Exception CurrentWebError;
        private FileCache cache;

        /// <summary>
        /// Constructor with caching
        /// </summary>
        /// <param name="c">the cache</param>
        public District(FileCache c)
        {
            cache = c;
        }

        /// <summary>
        /// Constructor without caching
        /// </summary>
        public District()
        {
        }

        /// <summary>
        /// Gets the district name for the given district.
        /// </summary>
        /// <param name="district">The district (e.g., pnw)</param>
        /// <returns>Task of type ObservableCollection of TeamEventInformation</returns>
        public async Task<string> GetDistrictName(string district)
        {
            Uri uri = new Uri(Hardcodes.BaseDistrictsURL + Hardcodes.YearString);
            string cachekey = "GetDistrictName" + Hardcodes.YearString;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);
                List<DistrictInformation> districtInfo = JsonConvert.DeserializeObject<List<DistrictInformation>>(responseData);
                return districtInfo.Where(d => d.key == district).Select(d => d.name).First();
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the district ranking information for the given district.
        /// </summary>
        /// <param name="district">The district (e.g., pnw)</param>
        /// <returns>Task of type ObservableCollection of TeamEventInformation</returns>
        public async Task<List<EventInformation>> GetDistrictEventsList(string district)
        {
            Uri uri = new Uri(Hardcodes.BaseDistrictURL + district + "/" + Hardcodes.YearString + "/events");
            string cachekey = "GetDistrictEventsList" + Hardcodes.YearString + district;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);
                List<EventInformation> districtEventInfo = JsonConvert.DeserializeObject<List<EventInformation>>(responseData);
                return districtEventInfo;
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<EventInformation>();
            }
        }

        /// <summary>
        /// Gets the district ranking information for the given district.
        /// </summary>
        /// <param name="district">The district (e.g., pnw)</param>
        /// <returns>Task of type ObservableCollection of TeamEventInformation</returns>
        public async Task<List<DistrictRankingInformation>> GetDistrictRankingList(string district)
        {
            Uri uri = new Uri(Hardcodes.BaseDistrictURL + district + "/" + Hardcodes.YearString + "/rankings");
            string cachekey = "GetDistrictRankingList" + Hardcodes.YearString + district;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);

                List<DistrictRankingInformation> districtRankingInfo = JsonConvert.DeserializeObject<List<DistrictRankingInformation>>(responseData);

                // Get the Event Information so that we can figure out the order of events returned by the ranking list
                List<EventInformation> eventInfo = await GetDistrictEventsList(district);

                // Go through each item and make sure we know which events are Event 1, Event 2, and District Championship
                foreach (var rank in districtRankingInfo)
                {
                    //look at each item and update the object with sorted events.
                    var keys = rank.event_points.Keys;
                    Dictionary<string, string> eventDateList = new Dictionary<string, string>();
                    foreach (var key in keys)
                    {
                        var startdate = eventInfo.Where(e => e.key.CompareTo(key) == 0).Select(e => e.start_date).First();
                        eventDateList.Add(key, startdate);
                    }

                    int i = 0;
                    foreach (var item in eventDateList.OrderBy(d => d.Value).Select(d => d))
                    {
                        if (i == 0) // Store Event 1 key
                        {
                            rank.DistrictEvent1Key = item.Key;
                        }
                        else if (i == 1) // Store Event 2 key 
                        {
                            rank.DistrictEvent2Key = item.Key;
                        }
                        else if (i == 2) // Store District Championship Event key 
                        {
                            rank.DistrictChampionshipKey = item.Key;
                        }
                        i++;
                    }
                }

                return districtRankingInfo;
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<DistrictRankingInformation>();
            }
        }
    }
}
