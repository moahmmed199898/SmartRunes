using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.LCU.Types;
using Newtonsoft.Json;
namespace NewLeagueApp.LCU {
    class Champions:RiotConnecter {
        public championData championsInformation;
        public Champions() {
            championsInformation = GetChampInfo();
        }
        public async Task<string> GetCurrentChamp() {
            var currentChampIDString = await SendRequestToRiot(LCUSharp.HttpMethod.Get, "lol-champ-select/v1/current-champion");
            var currentChampID = int.Parse(currentChampIDString);
            var currentChampName = (from champ in championsInformation.Data where champ.Value.Id == currentChampID select champ.Value.Name).Single();
            return currentChampName;
        }

        private championData GetChampInfo() {
            var jsonstring = File.ReadAllText("static/champion.json");
            return JsonConvert.DeserializeObject<championData>(jsonstring);
        }
    }
}
