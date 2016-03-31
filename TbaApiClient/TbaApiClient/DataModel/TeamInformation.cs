using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    public class TeamInformation
    {
        /// <summary>
        /// Returns the team number as a number instead of a string (e.g., for sorting)
        /// </summary>
        public int teamnum
        {
            get
            {
                int i;
                int.TryParse(this.team_number, out i);
                return i;
            }
        }

        public string website { get; set; }
        public string name { get; set; }
        public string locality { get; set; }
        public string rookie_year { get; set; }
        public string region { get; set; }
        public string team_number { get; set; }
        public string location { get; set; }
        public string key { get; set; }
        public string country_name { get; set; }
        public object motto { get; set; }
        public string nickname { get; set; }
    }
}
