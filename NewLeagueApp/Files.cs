using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp
{

    
    class Files
    {
        public List<ProfileApiCalls.GameStatsStructure> stats;
        private List<bool> exists;
        private ProfileApiCalls.HistoryClass hist;
        private List<int> matchids;
        private ProfileApiCalls calls;
        private string summonerName;
        public Files(String summonerName)
        {
            this.summonerName = summonerName;
            calls = new ProfileApiCalls(summonerName);
            stats = new List<ProfileApiCalls.GameStatsStructure>();
        }

        /***
         * Determines if a match needs to be added via parralel arraylists
         * TODO: search by endindex or begintime
         */
        public async Task DetermineDifference()
        {           
            await GetHist();
            Console.WriteLine("Boop");
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
            /*        IEnumerable<int> scoreQuery =
            from score in scores
            where score > 80
            select score;*/
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
        public async Task UpdateFile()
        {
            //Console.WriteLine("sdfesd");
            await calls.WriteToMem(stats);
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
        public async Task GetFirstMatch()
        {
            
            await GetHist();
            ProfileApiCalls.GameStatsStructure temp = await (calls.MatchInfo(hist.matches[0].gameID));
           
           stats.Add(temp);
           Console.WriteLine("i'm done!!!!");
            //Console.WriteLine(stats[0].participantIdentities[0].player.summonerName);
            // stats.Add(new ProfileApiCalls.GameStatsStructure());
           await calls.WriteToMem(stats);
           Console.WriteLine("i'm an idiot");

        }


    }
}
