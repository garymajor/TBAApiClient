using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    public class EventAwardInformation
    {
        public class RecipientList
        {
            public int team_number { get; set; }
            public object awardee { get; set; }
        }

        public string event_key { get; set; }
        public int award_type { get; set; }
        public string name { get; set; }
        public List<RecipientList> recipient_list { get; set; }
        public int year { get; set; }
    }
}
