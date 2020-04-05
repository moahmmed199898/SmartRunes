using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.Client.Types;
using Newtonsoft.Json;
using NewLeagueApp.Client;
using NewLeagueApp.Tools;
namespace NewLeagueApp.Client.ChampionInfo {

    public class Champion:GameSession {
        /// <summary>
        /// Contains info about the champions which includes their stats ( damage,armor, ...etc) info about their story, image, and more
        /// </summary>
        protected championData championsInformation;
        protected sessionData gameSession;


        /// <summary>
        /// initialize the class and set championsInformation property 
        /// </summary>
        public new async Task Init() {
            this.championsInformation = await GetChampInfo();
            this.gameSession = await new GameSession().GetSessionData();
        }

        /// <summary>
        /// gets the champ info from the json file
        /// </summary>
        /// <returns>The champ info as championData</returns>
        private async Task<championData> GetChampInfo() {
            var jsonstring = await AsyncIO.ReadFileAsync("static/champion.json");
            return JsonConvert.DeserializeObject<championData>(jsonstring);
        }

        /// <summary>
        /// gets the champ name from the id
        /// </summary>
        /// <param name="id">the id of the champ</param>
        /// <returns>the champ name</returns>
        public string GetChampNameById(int id) {
            string champName = (from champ in championsInformation.Data where champ.Value.Key == id select champ.Value.Name).Single();
            return champName;
        }
    }
}
