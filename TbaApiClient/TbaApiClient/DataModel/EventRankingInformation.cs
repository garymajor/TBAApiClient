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
        public string rank { get; set; }
        public string team { get; set; }
        public string ranking_score { get; set; }
        public string auto { get; set; }
        public string scale_challenge { get; set; }
        public string goals { get; set; }
        public string defense { get; set; }
        public string record_w_l_t { get; set; }
        public string played { get; set; }
    }
}
