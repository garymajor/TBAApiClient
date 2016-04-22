using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TbaApiClient.Cache;
using TbaApiClient.DataModel;

namespace TbaApiClient
{
    public class Team
    {
        public Exception CurrentWebError;
        private FileCache cache;

        /// <summary>
        /// Constructor with caching
        /// </summary>
        /// <param name="c">the cache</param>
        public Team(FileCache c)
        {
            cache = c;
        }

        /// <summary>
        /// Constructor without caching
        /// </summary>
        public Team()
        {
        }

        /// <summary>
        /// Gets the team event information for the given teamnumber.
        /// </summary>
        /// <param name="teamnumber">The team number (e.g., 2147)</param>
        /// <returns>Task of type ObservableCollection of TeamEventInformation</returns>
        public async Task<List<EventInformation>> GetTeamEventInfoList(string teamnumber)
        {
            Uri uri = new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber + "/" + Hardcodes.YearString + "/events");
            string cachekey = "GetTeamEventInfoList" + "-" + Hardcodes.TeamPrefix + teamnumber + "-" + Hardcodes.YearString;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);

                List<EventInformation> teamEventInfo = JsonConvert.DeserializeObject<List<EventInformation>>(responseData);
                return teamEventInfo;
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<EventInformation>();
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
            Uri uri = new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber + "/event/" + eventkey + "/matches");
            string cachekey = "GetTeamMatchList" + "-" + Hardcodes.TeamPrefix + teamnumber + "-" + eventkey;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);

                List<MatchInformation> eventMatchInfo = JsonConvert.DeserializeObject<List<MatchInformation>>(responseData);
                return eventMatchInfo;
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
            Uri uri = new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber);
            string cachekey = "GetTeamInfo" + "-" + Hardcodes.TeamPrefix + teamnumber;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);

                TeamInformation teamInfo = JsonConvert.DeserializeObject<TeamInformation>(responseData);
                return teamInfo;
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new TeamInformation();
            }
        }

        /// <summary>
        /// Gets the district for a given team.
        /// </summary>
        /// <param name="teamnumber">The team number (e.g., "2147")</param>
        /// <returns>Task of type String</string></returns>
        public async Task<string> GetTeamDistrict(string teamnumber)
        {
            Uri uri = new Uri(Hardcodes.BaseTeamURL + Hardcodes.TeamPrefix + teamnumber + "/history/districts");
            string cachekey = "GetTeamDistrict" + "-" + Hardcodes.TeamPrefix + teamnumber;
            CurrentWebError = null;

            try
            {
                var responseData = await ApiHelper.GetResponseFromUriOrCache(uri, cache, cachekey);

                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(responseData);
                string s = jsonObject.Value<string>(Hardcodes.YearString);
                s = s.Replace(Hardcodes.YearString, "");

                return (s != null) ? s : string.Empty;
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return string.Empty;
            }
        }
    }
}
