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
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<double> getCSM()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            return match.participants[playerIndex].stats.totalMinionsKilled / (double)(match.gameDuration/60);
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<double> getDPM()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            return match.participants[playerIndex].stats.totalDamageDealtToChampions / (double)(match.gameDuration/60);
        }



    }
}
