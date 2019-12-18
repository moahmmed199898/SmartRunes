using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp
{

    
    class File
    {
        private List<ProfileApiCalls.GameStatsStructure> stats { get => stats; set=> stats = value; }
        private List<bool> exists;
        private ProfileApiCalls.HistoryClass hist;
        private List<int> matchids;
        private ProfileApiCalls calls;
        private string summonerName;
        public File(String summonerName)
        {
            this.summonerName = summonerName;
            calls = new ProfileApiCalls(summonerName);

        }

        /***
         * Determines if a match needs to be added via parralel arraylists
         */
        public async Task DetermineDifference()
        {
            await GetHist();
            await ReadFromMemory();            
            for (int j = 0; j < hist.matches.Count; j++)
                {
                exists.Add(false);
                for (int i = 0; i < stats.Count; i++)
                {
                    if (hist.matches[j] == hist.matches[i])
                    {
                        exists[j] = true;
                    }
                }
            }
        }

        public async Task AddUnaddedMatches(int number)
        {
            int counter = 0;
            for(int i = 0; i < hist.matches.Count; i++)
            {
                if (!exists[i])
                {
                    ProfileApiCalls.GameStatsStructure temp = await (calls.MatchInfo(hist.matches[i].gameID));
                    stats.Add(temp);
                    counter++;
                    if (counter == number) break;
                }
            }
        }

        private async Task ReadFromMemory()
        {
            stats = await calls.ReadFromMem();
        }
        private async Task GetHist()
        {
            ProfileApiCalls.SummonerClass profile = await calls.SummonerInfo(summonerName);
            hist = await calls.HistoryInfo(profile.accountID);
        }


    }
}
