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
