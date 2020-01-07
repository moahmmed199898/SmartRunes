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
            if (currentChampIDString.Contains("\"httpStatus\":404,\"")) throw new Exception("No Active Game");
            var currentChampID = int.Parse(currentChampIDString);
            var currentChampName = (from champ in championsInformation.Data where champ.Value.Key == currentChampID select champ.Value.Name).Single();
            return currentChampName;
        }


        public string GetChampNameById(int id) {
            string champName = (from champ in championsInformation.Data where champ.Value.Key == id select champ.Key).Single();
            return champName;
        }

        public string GetChampImagePath(int id) {
            string champName = GetChampNameById(id);
            var path = $"static/img/splash/{champName}_0.jpg";
            return path;
        }
        public string GetChampImagePath(string name) {
            var path = $"static/img/splash/{name}_0.jpg";
            return path;
        }

        private championData GetChampInfo() {
            var jsonstring = File.ReadAllText("static/champion.json");
            return JsonConvert.DeserializeObject<championData>(jsonstring);
        }
    }
}
