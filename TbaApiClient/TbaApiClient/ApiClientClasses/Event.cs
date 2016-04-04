using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TbaApiClient.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace TbaApiClient
{
    public class Event
    {
        public Exception CurrentWebError;

        /// <summary>
        /// Gets the award list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of EventAwardInformation</returns>
        public async Task<ObservableCollection<EventAwardInformation>> GetEventAwardList(string eventkey)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/awards")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        ObservableCollection<EventAwardInformation> eventAwardInfo = JsonConvert.DeserializeObject<ObservableCollection<EventAwardInformation>>(responseData);
                        return eventAwardInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new ObservableCollection<EventAwardInformation>();
            }
        }

        /// <summary>
        /// Gets the ranking list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of EventRankingInformation</returns>
        public async Task<ObservableCollection<EventRankingInformation>> GetEventRankingList(string eventkey)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/rankings")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Fix up the output to shorten the names (can use as headers)
                        responseData = responseData.Replace("Ranking Score", "Score");
                        responseData = responseData.Replace("Rank", "");
                        responseData = responseData.Replace("Scale/Challenge", "Scl/Ch");
                        responseData = responseData.Replace("Record (W-L-T)", "Record");
                        responseData = responseData.Replace("Defense", "Def");
                        responseData = responseData.Replace("Played", "Pl");

                        // This API returns a JSonArray instead of JSonObjects -- so we have to deal with it differently than the other APIs.
                        JArray j = (JArray)JsonConvert.DeserializeObject(responseData);

                        List<EventRankingInformation> eventRankingInfo = new List<EventRankingInformation>();
                        foreach (var item in j)
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

                        return new ObservableCollection<EventRankingInformation>(eventRankingInfo);
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new ObservableCollection<EventRankingInformation>();
            }
        }

        /// <summary>
        /// Gets the team list for a given event.
        /// </summary>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of TeamInformation</returns>
        public async Task<ObservableCollection<TeamInformation>> GetEventTeamList(string eventkey)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/teams")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        ObservableCollection<TeamInformation> eventTeamInfo = JsonConvert.DeserializeObject<ObservableCollection<TeamInformation>>(responseData);
                        return eventTeamInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new ObservableCollection<TeamInformation>();
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
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

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
