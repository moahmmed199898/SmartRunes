using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.Client.Types;
using NewLeagueApp.Tools;
using Newtonsoft.Json;

namespace NewLeagueApp.Client.ChampionInfo {
    class EnamyChampFinder: Champion {
        public string[] GetEnamyChamps() {
            try {
                if (this.gameSessionData.TheirTeam == null || this.gameSessionData.TheirTeam.Count() == 0 || this.gameSessionData.TheirTeam[0].ChampionId == 0) {
                    string[] errorArray = { "NA" };
                    return errorArray;
                };
                var enamyChampIds = (from player in this.gameSessionData.TheirTeam select player.ChampionId).ToArray();
                var enamyChampNames = GetChampNamesByIDs(enamyChampIds);
                return enamyChampNames;
            } catch (Exception error) {
                throw error;
            }
        }

        public string[] GetChampNamesByIDs(int[] champIDs) {
            var champNames = new List<string>();
            foreach (var champId in champIDs) {
                champNames.Add(GetChampNameById(champId));
            }
            return champNames.ToArray();
        }


        public async Task<string> GetChampLanningAginst(string myLane) {
            var enamyChamps = GetEnamyChamps();
            if (enamyChamps[0] == "NA") return "NA";
            //guess enamy champion from the champion's most common role
            foreach (var enamyChamp in enamyChamps) {
                var lane = await GetChampLane(enamyChamp);
                if (lane == myLane) return enamyChamp;
            }
            //if it can't find the enamy champion it will look for it using their 2nd most common role
            foreach (var enamyChamp in enamyChamps) {
                var lane = await GetChamp2ndLane(enamyChamp);
                if (lane == myLane) return enamyChamp;
            }
            return "NA";
        }




        public async Task<string> GetChampLane(string champName) {
            try {
                var jsonString = await AsyncIO.ReadFileAsync("cache/champMatchUps.json");
                var champMatchUps = JsonConvert.DeserializeObject<Dictionary<string, ChampMatchups>>(jsonString);
                var lanes = champMatchUps[champName];
                //get the max value
                string maxLane = "";
                int maxGames = 0;
                foreach (var lane in lanes.GetType().GetFields()) {
                    var gamesPlayed = (int)lanes.GetType().GetField(lane.Name).GetValue(lanes);
                    if (gamesPlayed > maxGames) {
                        maxGames = gamesPlayed;
                        maxLane = lane.Name;
                    }
                }
                return maxLane;
            } catch (Exception error) {
                throw error;
            }
        }


        public async Task<string> GetChamp2ndLane(string champName) {
            try {
                var jsonString = await AsyncIO.ReadFileAsync("cache/champMatchUps.json");
                var champMatchUps = JsonConvert.DeserializeObject<Dictionary<string, ChampMatchups>>(jsonString);
                var lanes = champMatchUps[champName];
                //get the max value
                int FirstMaxGames = 0;
                string FirstMaxLane = "";
                string secoundMaxLane = "";
                int secoundMaxGames = 0;
                foreach (var lane in lanes.GetType().GetFields()) {
                    var gamesPlayed = (int)lanes.GetType().GetField(lane.Name).GetValue(lanes);
                    if (gamesPlayed > FirstMaxGames) {
                        FirstMaxGames = gamesPlayed;
                        FirstMaxLane = lane.Name;
                    }

                    if(gamesPlayed>secoundMaxGames && gamesPlayed < FirstMaxGames) {
                        secoundMaxGames = gamesPlayed;
                        secoundMaxLane = lane.Name;
                    }
                }
                return secoundMaxLane;
            } catch (Exception error) {
                throw error;
            }
        }


    }
}
