using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp.ProfilePage
{
    class MatchStats
    {  private ProfileApiCalls.GameStatsStructure match;
        private int playerIndex;
        public MatchStats(ProfileApiCalls.GameStatsStructure match, int playerIndex)
        {
            this.playerIndex = playerIndex;
            this.match = match;
        }
        public async Task<double> getCSM()
        {
            return match.participants[playerIndex].stats.totalMinionsKilled / match.gameDuration;
        }
        public async Task<double> getDPM()
        {
            return match.participants[playerIndex].stats.totalDamageDealtToChampions / match.gameDuration;
        }
    }
}
