using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp.ProfilePage
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
            hist = new ProfileApiCalls.HistoryClass();
            exists = new List<bool>();
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
            

        }

        public async Task AddUnaddedMatches(int number)
        {
            Console.WriteLine("Boop, the reckonning");
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
           await UpdateFile();
        }
        public async Task UpdateFile()
        {
            //Console.WriteLine("sdfesd");
            await calls.WriteToMem(stats);
        }

        public async Task<List<ProfileApiCalls.GameStatsStructure_participants>> GetSummonerStats(String summonerName)
        {
            await ReadFromMemory();
            List<ProfileApiCalls.GameStatsStructure_participants> summ = new List<ProfileApiCalls.GameStatsStructure_participants>();
           foreach (ProfileApiCalls.GameStatsStructure stat in stats)
            {
                for(int i = 0; i<9;i++)
                {
                   if(stat.participantIdentities[i].player.summonerName.Equals(summonerName))
                    {
                        summ.Add(stat.participants[i]);
                    }
                }
                
            }
            return summ;
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
            //Console.WriteLine(stats[0].participantIdentities[0].player.summonerName);
            // stats.Add(new ProfileApiCalls.GameStatsStructure());
           await calls.WriteToMem(stats);

        }


    }
}
