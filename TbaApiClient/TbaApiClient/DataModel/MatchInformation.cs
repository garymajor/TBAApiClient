using System.Collections.Generic;
using System.Text;

namespace TbaApiClient.DataModel
{
    public class MatchInformation
    {
        /// <summary>
        /// public member to be able to sort on the competition level (qual match=1, semi-finals=2, quarter-finals=3, finals=4, anything else=5
        /// </summary>
        public int CompetitionLevelSortOrder
        {
            get
            {
                switch (comp_level)
                {
                    case "qm": return 1;
                    case "sf": return 2;
                    case "qf": return 3;
                    case "f": return 4;
                }
                return 5;
            }
        }

        /// <summary>
        /// public member to get formatted competition level + match number (e.g., qm50)
        /// </summary>
        public string MatchNumber
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append(comp_level);
                s.Append(match_number);
                return s.ToString();
            }
        }

        /// <summary>
        /// public member to easily get the blue alliance teams for the match.
        /// </summary>
        public string BlueAllianceTeams
        {
            get
            {
                return string.Join(", ", alliances.blue.teams).Replace(Hardcodes.TeamPrefix,"");
            }
        }

        /// <summary>
        /// public member to easily get the blue alliance score for the match.
        /// </summary>
        public int BlueAllianceScore
        {
            get
            {
                return alliances.blue.score;
            }
        }

        /// <summary>
        /// public member to easily get the red alliance teams for the match.
        /// </summary>
        public string RedAllianceTeams
        {
            get
            {
                return string.Join(", ", alliances.red.teams).Replace(Hardcodes.TeamPrefix, "");
            }
        }

        /// <summary>
        /// public member to easily get the red alliance score for the match.
        /// </summary>
        public int RedAllianceScore
        {
            get
            {
                return alliances.red.score;
            }
        }

        public class Video
        {
            public string type { get; set; }
            public string key { get; set; }
        }

        public class Blue
        {
            public int teleopPoints { get; set; }
            public string robot3Auto { get; set; }
            public int breachPoints { get; set; }
            public int autoPoints { get; set; }
            public int teleopScalePoints { get; set; }
            public int autoBouldersLow { get; set; }
            public bool teleopTowerCaptured { get; set; }
            public int teleopBouldersLow { get; set; }
            public int teleopCrossingPoints { get; set; }
            public int foulCount { get; set; }
            public int foulPoints { get; set; }
            public string towerFaceB { get; set; }
            public string towerFaceC { get; set; }
            public string towerFaceA { get; set; }
            public int techFoulCount { get; set; }
            public int totalPoints { get; set; }
            public int adjustPoints { get; set; }
            public string position3 { get; set; }
            public string robot1Auto { get; set; }
            public string position4 { get; set; }
            public string position5 { get; set; }
            public int autoBoulderPoints { get; set; }
            public int teleopBoulderPoints { get; set; }
            public int teleopBouldersHigh { get; set; }
            public int autoBouldersHigh { get; set; }
            public string robot2Auto { get; set; }
            public int position1crossings { get; set; }
            public int towerEndStrength { get; set; }
            public int position4crossings { get; set; }
            public int position2crossings { get; set; }
            public int position5crossings { get; set; }
            public int position3crossings { get; set; }
            public int teleopChallengePoints { get; set; }
            public int autoCrossingPoints { get; set; }
            public bool teleopDefensesBreached { get; set; }
            public int autoReachPoints { get; set; }
            public string position2 { get; set; }
            public int capturePoints { get; set; }
        }

        public class Red
        {
            public int teleopPoints { get; set; }
            public string robot3Auto { get; set; }
            public int breachPoints { get; set; }
            public int autoPoints { get; set; }
            public int teleopScalePoints { get; set; }
            public int autoBouldersLow { get; set; }
            public bool teleopTowerCaptured { get; set; }
            public int teleopBouldersLow { get; set; }
            public int teleopCrossingPoints { get; set; }
            public int foulCount { get; set; }
            public int foulPoints { get; set; }
            public string towerFaceB { get; set; }
            public string towerFaceC { get; set; }
            public string towerFaceA { get; set; }
            public int techFoulCount { get; set; }
            public int totalPoints { get; set; }
            public int adjustPoints { get; set; }
            public string position3 { get; set; }
            public string robot1Auto { get; set; }
            public string position4 { get; set; }
            public string position5 { get; set; }
            public int autoBoulderPoints { get; set; }
            public int teleopBoulderPoints { get; set; }
            public int teleopBouldersHigh { get; set; }
            public int autoBouldersHigh { get; set; }
            public string robot2Auto { get; set; }
            public int position1crossings { get; set; }
            public int towerEndStrength { get; set; }
            public int position4crossings { get; set; }
            public int position2crossings { get; set; }
            public int position5crossings { get; set; }
            public int position3crossings { get; set; }
            public int teleopChallengePoints { get; set; }
            public int autoCrossingPoints { get; set; }
            public bool teleopDefensesBreached { get; set; }
            public int autoReachPoints { get; set; }
            public string position2 { get; set; }
            public int capturePoints { get; set; }
        }

        public class ScoreBreakdown
        {
            public Blue blue { get; set; }
            public Red red { get; set; }
        }

        public class Blue2
        {
            public int score { get; set; }
            public List<string> teams { get; set; }
        }

        public class Red2
        {
            public int score { get; set; }
            public List<string> teams { get; set; }
        }

        public class Alliances
        {
            public Blue2 blue { get; set; }
            public Red2 red { get; set; }
        }

        public string comp_level { get; set; }
        public int match_number { get; set; }
        public List<Video> videos { get; set; }
        public object time_string { get; set; }
        public int set_number { get; set; }
        public string key { get; set; }
        public int time { get; set; }
        public ScoreBreakdown score_breakdown { get; set; }
        public Alliances alliances { get; set; }
        public string event_key { get; set; }

    }
}
