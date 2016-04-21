using Newtonsoft.Json;
using TbaApiClient.DataModel;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace TbaApiClient
{
    public class District
    {
        public Exception CurrentWebError;

        /// <summary>
        /// Gets the district ranking information for the given district.
        /// </summary>
        /// <param name="district">The district (e.g., pnw)</param>
        /// <returns>Task of type ObservableCollection of TeamEventInformation</returns>
        public async Task<string> GetDistrictName(string district)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    //using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseDistrictURL + Hardcodes.YearString)))
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseDistrictsURL + Hardcodes.YearString)))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        List<DistrictInformation> districtInfo = JsonConvert.DeserializeObject<List<DistrictInformation>>(responseData);
                        return districtInfo.Where(d => d.key == district).Select(d => d.name).First();
                    }
                }
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
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseDistrictURL + district + "/" + Hardcodes.YearString + "/events")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        List<EventInformation> districtEventInfo = JsonConvert.DeserializeObject<List<EventInformation>>(responseData);
                        return districtEventInfo;
                    }
                }
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
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseDistrictURL + district + "/" + Hardcodes.YearString + "/rankings")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
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
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<DistrictRankingInformation>();
            }
        }
    }
}
