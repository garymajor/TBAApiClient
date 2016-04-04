using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TbaApiClient.DataModel
{
    [DataContract]
    public class EventRankingInformation
    {
        [DataMember]
        public string Rank { get; set; }
        [DataMember]
        public string Team { get; set; }
        [DataMember]
        public string Ranking_Score { get; set; }
        [DataMember]
        public string Auto { get; set; }
        [DataMember]
        public string Scale_Challenge { get; set; }
        [DataMember]
        public string Goals { get; set; }
        [DataMember]
        public string Defense { get; set; }
        [DataMember]
        public string Record_W_L_T { get; set; }
        [DataMember]
        public string Played { get; set; }
    }
}
