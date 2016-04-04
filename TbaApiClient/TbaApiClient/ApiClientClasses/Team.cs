using Newtonsoft.Json;
using TbaApiClient.DataModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace TbaApiClient
{
    public class Team
    {
        public Exception CurrentWebError;

        /// <summary>
        /// Gets the team event information for the given teamnumber.
        /// </summary>
        /// <param name="teamnumber">The team number (e.g., 2147)</param>
        /// <returns>Task of type ObservableCollection of TeamEventInformation</returns>
        public async Task<ObservableCollection<EventInformation>> GetTeamEventInfoList(string teamnumber)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber + "/" + Hardcodes.YearString + "/events")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        ObservableCollection<EventInformation> teamEventInfo = JsonConvert.DeserializeObject<ObservableCollection<EventInformation>>(responseData);
                        return teamEventInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new ObservableCollection<EventInformation>();
            }
        }

        /// <summary>
        /// Gets the match list for a given event.
        /// </summary>
        /// <param name="teamnumber">The team to filter matches to (e.g., "2147")</param>
        /// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        /// <returns>Task of type ObservableCollection of MatchInformation</returns>
        public async Task<List<MatchInformation>> GetTeamEventMatchList(string teamnumber, string eventkey)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber + "/event/" + eventkey + "/matches")))
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
        /// Gets the team information for the given teamnumber.
        /// </summary>
        /// <param name="teamnumber">The team number (e.g., 2147)</param>
        /// <returns>Task of type TeamInformation</returns>
        public async Task<TeamInformation> GetTeamInfo(string teamnumber)
        {
            try
            {
                CurrentWebError = null;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber)))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        TeamInformation teamInfo = JsonConvert.DeserializeObject<TeamInformation>(responseData);
                        return teamInfo;
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new TeamInformation();
            }
        }
    }
}
