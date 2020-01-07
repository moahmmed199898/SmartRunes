using LCUSharp;
using NewLeagueApp.LCU.Types;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLeagueApp.LCU.Runes;
namespace NewLeagueApp.LCU {
    class LCU:RiotConnecter {
        private long summonerID; 
        public LCU() {
            init();
        }

        public async void init() {
            //var runes = new SmartRunes();
            //summonerID = await GetCurrentSummnerID();
            //runes.AutoRuneSetter();
        }

        public async Task<string> GetDeclaredLane() {
            var stringJSON = File.ReadAllText("static/TempDraftPick.json");
            var data = JsonConvert.DeserializeObject<sessionData>(stringJSON);
            var lane = (from player in data.MyTeam where player.SummonerId == summonerID select player.AssignedPosition).Single();
            return lane;
            throw new WarningException("Using a static file to get draft pick information");
        }

        public async Task<long> GetCurrentSummnerID() {
            var jsonString = await SendRequestToRiot(HttpMethod.Get, " /lol-summoner/v1/current-summoner");
            var data = JsonConvert.DeserializeObject<SummonerInfo>(jsonString);
            return data.SummonerId;
        }

    }
}
