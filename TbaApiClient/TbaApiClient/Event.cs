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
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ObservableCollection<EventAwardInformation>));
                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseData)))
                        {
                            ObservableCollection<EventAwardInformation> eventAwardInfo = (ObservableCollection<EventAwardInformation>)serializer.ReadObject(ms); // serialize the data into TeamData
                            CurrentWebError = null;
                            return eventAwardInfo;
                        }
                    }
                }
            }
            catch (Exception webError)
            {
                CurrentWebError = webError;
                return new ObservableCollection<EventAwardInformation>();
            }
        }

        ///// <summary>
        ///// Gets the ranking list for a given event.
        ///// </summary>
        ///// <param name="eventkey">The event key (e.g., "2016waspo")</param>
        ///// <returns>Task of type ObservableCollection of EventRankingInformation</returns>
        //public async Task<ObservableCollection<EventRankingInformation>> GetEventRankingList(string eventkey)
        //{
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("accept", "application/json");
        //            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-TBA-App-Id", Hardcodes.AppID);

        //            using (var response = await httpClient.GetAsync(new Uri(Hardcodes.BaseEventURL + eventkey + "/rankings")))
        //            {
        //                string responseData = await response.Content.ReadAsStringAsync();
        //                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ObservableCollection<EventRankingInformation>));
        //                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseData)))
        //                {
        //                    ObservableCollection<EventRankingInformation> eventRankingInfo = (ObservableCollection<EventRankingInformation>)serializer.ReadObject(ms); // serialize the data into TeamData
        //                    CurrentWebError = null;
        //                    return eventRankingInfo;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception webError)
        //    {
        //        CurrentWebError = webError;
        //        return new ObservableCollection<EventRankingInformation>();
        //    }
        //}

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
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ObservableCollection<TeamInformation>));
                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseData)))
                        {
                            ObservableCollection<TeamInformation> eventTeamInfo = (ObservableCollection<TeamInformation>)serializer.ReadObject(ms); // serialize the data into TeamData
                            CurrentWebError = null;
                            return eventTeamInfo;
                        }
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
        public async Task<EventInformation> GetEventnfo(string eventkey)
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
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(EventInformation));
                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseData)))
                        {
                            EventInformation eventinfo = (EventInformation)serializer.ReadObject(ms); // serialize the data into TeamData
                            CurrentWebError = null;
                            return eventinfo;
                        }
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
