using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TbaApiClient.Cache;
using TbaApiClient.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace TbaApiClient
{
    public class Event
    {
        public Exception CurrentWebError;
        private FileCache cache;

        /// <summary>
        /// Constructor with caching
        /// </summary>
        /// <param name="c">the cache</param>
        public Event(FileCache c)
        {
            cache = c;
        }

        /// <summary>
        /// Constructor without caching
        /// </summary>
        public Event()
        {
        }

        /// <summary>
        /// Gets the award list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of EventAwardInformation</returns>
        public async Task<List<EventAwardInformation>> GetEventAwardList(string eventkey)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/awards")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        List<EventAwardInformation> eventAwardInfo = JsonConvert.DeserializeObject<List<EventAwardInformation>>(responseData);
                        return eventAwardInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<EventAwardInformation>();
            }
        }

        /// <summary>
        /// Gets the match list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of MatchInformation</returns>
        public async Task<List<MatchInformation>> GetEventMatchList(string eventkey)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/matches")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        List<MatchInformation> eventMatchInfo = JsonConvert.DeserializeObject<List<MatchInformation>>(responseData);
                        return eventMatchInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<MatchInformation>();
            }
        }

        /// <summary>
        /// Gets the ranking list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of EventRankingInformation</returns>
        public async Task<List<EventRankingInformation>> GetEventRankingList(string eventkey)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/rankings")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        // This API returns a JSonArray instead of JSonObjects -- so we have to deal with it differently than the other APIs.
                        JArray j = (JArray)JsonConvert.DeserializeObject(responseData);

                        List<EventRankingInformation> eventRankingInfo = new List<EventRankingInformation>();
                        foreach (var item in j)
                        {
                            // check to see if this is the "header" row by checking the team string for the header string - we don't want to add this to the List<>
                            if (!item[1].ToString().Equals("Team"))
                            {
                                EventRankingInformation e = new EventRankingInformation();
                                e.rank = item[0].ToString();
                                e.team = item[1].ToString();
                                e.ranking_score = item[2].ToString();
                                e.auto = item[3].ToString();
                                e.scale_challenge = item[4].ToString();
                                e.goals = item[5].ToString();
                                e.defense = item[6].ToString();
                                e.record_w_l_t = item[7].ToString();
                                e.played = item[8].ToString();
                                eventRankingInfo.Add(e);
                            }
                        }

                        return new List<EventRankingInformation>(eventRankingInfo);
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<EventRankingInformation>();
            }
        }

        /// <summary>
        /// Gets the team list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of TeamInformation</returns>
        public async Task<List<TeamInformation>> GetEventTeamList(string eventkey)
        {
            try
            {
                CurrentWebError = null;

                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/teams")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        List<TeamInformation> eventTeamInfo = JsonConvert.DeserializeObject<List<TeamInformation>>(responseData);
                        return eventTeamInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<TeamInformation>();
            }
        }

        /// <summary>
        /// Gets the event information for the given eventkey.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., 2017waspo)</param>
        /// <returns>Task of type EventInformation</returns>
        public async Task<EventInformation> GetEventInfo(string eventkey)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = ApiHelper.GetHttpClientWithCaching())
                {
                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey)))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        EventInformation eventInfo = JsonConvert.DeserializeObject<EventInformation>(responseData);
                        return eventInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new EventInformation();
            }
        }
    }
}
