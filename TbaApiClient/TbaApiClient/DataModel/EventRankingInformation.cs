using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    public class EventRankingInformation
    {
        public string Rank { get; set; }
        public string Team { get; set; }
        public string Ranking_Score { get; set; }
        public string Auto { get; set; }
        public string Scale_Challenge { get; set; }
        public string Goals { get; set; }
        public string Defense { get; set; }
        public string Record_W_L_T { get; set; }
        public string Played { get; set; }
    }
}
