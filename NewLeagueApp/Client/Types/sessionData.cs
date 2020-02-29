using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp.Client.Types {
  
    public partial class sessionData {
        public Bans Bans { get; set; }
        public List<Team> MyTeam { get; set; }
        public List<Team> TheirTeam { get; set; }
        public Timer Timer { get; set; }
    }

    public partial class Bans {
        public List<object> MyTeamBans { get; set; }
        public long NumBans { get; set; }
        public List<object> TheirTeamBans { get; set; }
    }
    public partial class Team {
        public string AssignedPosition { get; set; }
        public long CellId { get; set; }
        public int ChampionId { get; set; }
        public long ChampionPickIntent { get; set; }
        public string EntitledFeatureType { get; set; }
        public string PlayerType { get; set; }
        public long SelectedSkinId { get; set; }
        public double Spell1Id { get; set; }
        public double Spell2Id { get; set; }
        public long SummonerId { get; set; }
        public long TeamTeam { get; set; }
        public long WardSkinId { get; set; }
    }

    public partial class Timer {
        public long AdjustedTimeLeftInPhase { get; set; }
        public long AdjustedTimeLeftInPhaseInSec { get; set; }
        public long InternalNowInEpochMs { get; set; }
        public bool IsInfinite { get; set; }
        public string Phase { get; set; }
        public long TimeLeftInPhase { get; set; }
        public long TimeLeftInPhaseInSec { get; set; }
        public long TotalTimeInPhase { get; set; }
    }

}
