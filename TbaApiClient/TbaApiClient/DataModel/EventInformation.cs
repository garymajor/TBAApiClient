using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    public class EventInformation
    {
        public class Webcast
        {
            public string type { get; set; }
            public string channel { get; set; }
        }

        public class Alliance
        {
            public List<object> declines { get; set; }
            public List<string> picks { get; set; }
        }

        public string key { get; set; }
        public string website { get; set; }
        public bool official { get; set; }
        public string end_date { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public object facebook_eid { get; set; }
        public string event_district_string { get; set; }
        public string venue_address { get; set; }
        public int event_district { get; set; }
        public string location { get; set; }
        public string event_code { get; set; }
        public int year { get; set; }
        public List<Webcast> webcast { get; set; }
        public List<Alliance> alliances { get; set; }
        public string event_type_string { get; set; }
        public string start_date { get; set; }
        public int event_type { get; set; }
    }
}
