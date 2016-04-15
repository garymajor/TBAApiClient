using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    public class DistrictRankingInformation
    {
        public string TeamNumber
        {
            get
            {
                return team_key.Replace(Hardcodes.TeamPrefix, "");
            }
        }
        
        public class EventPoints
        {
            public int alliance_points { get; set; }
            public int award_points { get; set; }
            public int elim_points { get; set; }
            public bool district_cmp { get; set; }
            public int total { get; set; }
            public int qual_points { get; set; }
        }

        public int point_total { get; set; }
        public string team_key { get; set; }
        public Dictionary<string, EventPoints> event_points { get; set; }
        public int rank { get; set; }
        public int rookie_bonus { get; set; }
    }
}
