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

        public string DistrictEvent1Key { get; set; }
        public string DistrictEvent2Key { get; set; }
        public string DistrictChampionshipKey { get; set; }

        public EventPoints DistrictEvent1
        {
            get
            {
                if (!string.IsNullOrEmpty(DistrictEvent1Key))
                {
                    if (event_points.Keys.Contains(DistrictEvent1Key))
                    {
                        return event_points[DistrictEvent1Key];
                    }
                }
                return new EventPoints();
            }
        }

        public EventPoints DistrictEvent2
        {
            get
            {
                if (!string.IsNullOrEmpty(DistrictEvent2Key))
                {
                    if (event_points.Keys.Contains(DistrictEvent2Key))
                    {
                        return event_points[DistrictEvent2Key];
                    }
                }
                return new EventPoints();
            }
        }

        public EventPoints DistrictChampionship
        {
            get
            {
                if (!string.IsNullOrEmpty(DistrictChampionshipKey))
                {
                    if (event_points.Keys.Contains(DistrictChampionshipKey))
                    {
                        return event_points[DistrictChampionshipKey];
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
