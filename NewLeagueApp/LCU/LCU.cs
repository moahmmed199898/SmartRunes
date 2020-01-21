using LCUSharp;
using NewLeagueApp.LCU.Types;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLeagueApp.LCU.Runes;
using System;

namespace NewLeagueApp.LCU {
    class LCU:RiotConnecter {
        private long summonerID; 
        public LCU() {
            init();
        }

        public async Task init() {
            summonerID = await GetCurrentSummnerID();
        }

        public async Task<sessionData> GetSessionData() {
            /*var stringJSON = File.ReadAllText("static/TempDraftPick.json");*/
            var stringJSON = await SendRequestToRiot(HttpMethod.Get, "/lol-champ-select-legacy/v1/session");
            stringJSON = stringJSON.Replace("UTILITY", "SUPP");
            var data = JsonConvert.DeserializeObject<sessionData>(stringJSON);
            return data;
        }
        public async Task<string> GetDeclaredLane() {
            try {
                var data = await GetSessionData();
                var laneArray = from player in data.MyTeam where player.SummonerId == summonerID select player.AssignedPosition;
                if (laneArray.Count() <= 0) return "NA";
                var lane = laneArray.Single();
                if (lane == "") return "NA";
                if (lane == "") return "UTILITY";
                return lane;
            } catch(Exception error) {
                throw error;
            }
            throw new WarningException("Using a static file to get draft pick information");
        }

        public async Task<long> GetCurrentSummnerID() {
            var jsonString = await SendRequestToRiot(HttpMethod.Get, " /lol-summoner/v1/current-summoner");
            var data = JsonConvert.DeserializeObject<SummonerInfo>(jsonString);
            return data.SummonerId;
        }

    }
}
