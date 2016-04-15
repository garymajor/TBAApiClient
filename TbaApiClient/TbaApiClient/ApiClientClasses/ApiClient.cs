using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient
{
    /// <summary>
    /// The Blue Alliance API Client class
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// District API Class
        /// </summary>
        public District DistrictApi
        {
            get
            {
                return districtApi;
            }
        }

        /// <summary>
        /// Event API Class
        /// </summary>
        public Event EventApi
        {
            get
            {
                return eventApi;
            }
        }

        /// <summary>
        /// Event API Class
        /// </summary>
        public Team TeamApi
        {
            get
            {
                return teamApi;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ApiClient()
        {
            eventApi = new Event();
            teamApi = new Team();
            districtApi = new District();
        }

        /// <summary>
        /// private District API member
        /// </summary>
        private District districtApi { get; set; }

        /// <summary>
        /// private Event API member
        /// </summary>
        private Event eventApi { get; set; }

        /// <summary>
        /// private Team API member
        /// </summary>
        private Team teamApi { get; set; }
    }
}
