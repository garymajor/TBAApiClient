using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient
{
    public static class Hardcodes
    {
        /// <summary>
        /// The application ID used in TheBlueAlliance API calls (placed in the http header).
        /// </summary>
        public const string AppID = "team2147/gary-major:TheBlueAlliance v2 API Client:1.0.0.0";

        /// <summary>
        /// The base URL that is used to make calls to TheBlueAlliance API.
        /// </summary>
        public const string BaseURL = "http://www.thebluealliance.com/api/v2/";

        /// <summary>
        /// The  URL that is used to make calls to TheBlueAlliance Team API.
        /// </summary>
        public const string BaseTeamURL = BaseURL + "team/";

        /// <summary>
        /// The  URL that is used to make calls to TheBlueAlliance Team API.
        /// </summary>
        public const string BaseEventURL = BaseURL + "event/";

        /// <summary>
        /// Prefix to put in front of team in The Blue Alliance API calls.
        /// </summary>
        public const string TeamPrefix = "frc";

        /// <summary>
        /// The default year to use with The Blue Alliance API.
        /// </summary>
        public const string YearString = "2016";
    }
}
