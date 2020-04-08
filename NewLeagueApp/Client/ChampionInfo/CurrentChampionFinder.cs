using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.Client.Types;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
namespace NewLeagueApp.Client.ChampionInfo {
    public class CurrentChampionFinder: Champion {
        /// <summary>
        /// Deals with everything regarding champions. 
        /// <para>
        /// When you initialize this class make sure you call Init() or you will not be able to use championsInformation property 
        /// </para>
        /// </summary>
        private String currentChamp;

        public CurrentChampionFinder() {
        }
        
        /// <summary>
        /// (Do Not Use)
        /// Gets the current champion from the client or throws "No Active Game" if no game is active yet 
        /// </summary>
        public async Task<string> GetCurrentChamp() {
            try {
                /*                if (this.currentChamp != null) return this.currentChamp;
                                var currentChampIDString = await gameSession.SendRequestToRiot(LCUSharp.HttpMethod.Get, "lol-champ-select/v1/current-champion");
                                if (currentChampIDString.Contains("\"httpStatus\":404,\"")) { await Task.Delay(2000); return await GetCurrentChamp(); };
                                var currentChampID = int.Parse(currentChampIDString);
                                if (currentChampID == 0) { await Task.Delay(2000); return await GetCurrentChamp(); };
                                var currentChampName = (from champ in championsInformation.Data where champ.Value.Key == currentChampID select champ.Value.Name).Single();
                                this.currentChamp = currentChampName;
                                return currentChampName;*/
                var summnerID = await gameSession.GetCurrentSummnerID();
                var IcurrentChampID = from player in this.gameSessionData.MyTeam where player.SummonerId == summnerID select player.ChampionId;
                if (IcurrentChampID.Count() == 0) { await Task.Delay(2000); return await GetCurrentChamp(); };
                var currentChampID = IcurrentChampID.Single<int>();
                if (currentChampID == 0) { await Task.Delay(2000); return await GetCurrentChamp(); };
                var currentChampName = (from champ in championsInformation.Data where champ.Value.Key == currentChampID select champ.Value.Name).Single();
                this.currentChamp = currentChampName;
                return currentChampName;
            } catch(Exception error) {
                throw error;
            }
        }



    }
}
