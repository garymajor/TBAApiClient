using TbaApiClient.DataModel;
using System;
using System.IO;
using System.Collections.Generic;
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
        /// <returns>Task of type TeamEventInformation</returns>
        public async Task<List<TeamEventInformation>> GetTeamEventInfoList(string teamnumber)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseURL + Hardcodes.TeamPrefix + teamnumber + "/" + Hardcodes.YearString + "/events")))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<TeamEventInformation>));
                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseData)))
                        {
                            List<TeamEventInformation> teamEventInfo = (List<TeamEventInformation>)serializer.ReadObject(ms); // serialize the data into TeamData
                            CurrentWebError = null;
                            return teamEventInfo;
                        }
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new List<TeamEventInformation>();
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
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

                    using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseURL + Hardcodes.TeamPrefix + teamnumber)))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TeamInformation));
                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseData)))
                        {
                            TeamInformation teaminfo = (TeamInformation)serializer.ReadObject(ms); // serialize the data into TeamData
                            CurrentWebError = null;
                            return teaminfo;
                        }
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
