using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    public class TeamInformation
    {
        public string website { get; set; }
        public string name { get; set; }
        public string locality { get; set; }
        public int rookie_year { get; set; }
        public string region { get; set; }
        public int team_number { get; set; }
        public string location { get; set; }
        public string key { get; set; }
        public string country_name { get; set; }
        public object motto { get; set; }
        public string nickname { get; set; }
    }
}
