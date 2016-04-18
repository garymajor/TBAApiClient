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

        public EventPoints DistrictEvent1
        {
            get
            {
                if (event_points.Keys.Count > 0)
                {
                    var keys = event_points.Keys;
                    var key = keys.ElementAt(0);

                    if (event_points[key] != null)
                    {
                        return event_points[key];
                    }
                }
                return new EventPoints();
            }
        }

        public EventPoints DistrictEvent2
        {
            get
            {
                if (event_points.Keys.Count > 1)
                {
                    var keys = event_points.Keys;
                    var key = keys.ElementAt(1);

                    if (event_points[key] != null)
                    {
                        return event_points[key];
                    }
                }
                return new EventPoints();
            }
        }

        public EventPoints DistrictChampionship
        {
            get
            {
                if (event_points.Keys.Count > 2)
                {
                    var keys = event_points.Keys;
                    var key = keys.ElementAt(2);

                    if (event_points[key] != null)
                    {
                        return event_points[key];
                    }
                }
                return new EventPoints();
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
