﻿using TbaApiClient.DataModel;
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
        private string BaseURL = "http://www.thebluealliance.com/api/v2/team/";
        private string AppID = "team2147/gary-major:TheBlueAlliance v2 API Client:1.0.0.0";
        public Exception CurrentWebError;

        public async Task<TeamInformation> GetTeamInfo(string teamNumber)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", AppID);

                    using (var response = await httpClient.GetAsync(new Uri(BaseURL + "frc" + teamNumber)))
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
