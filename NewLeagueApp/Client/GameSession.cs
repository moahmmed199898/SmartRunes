using LCUSharp;
using NewLeagueApp.Client.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp.Client {
    public class GameSession: LCU{
        public async Task<sessionData> GetSessionData() {
            //var stringJSON = await SendRequestToRiot(HttpMethod.Get, "/lol-champ-select/v1/session");
            var stringJSON = System.IO.File.ReadAllText("static/tempDraftPick.json");
            Logger.LogSessionData(stringJSON);
            stringJSON = stringJSON.Replace("utility", "SUPP");
            stringJSON = stringJSON.Replace("bottom", "ADC");
            stringJSON = stringJSON.Replace("top", "TOP");
            stringJSON = stringJSON.Replace("middle", "MID");
            stringJSON = stringJSON.Replace("jungle", "JUNGLE");
            var data = JsonConvert.DeserializeObject<sessionData>(stringJSON);
            return data;
        }
        public async Task<string> GetDeclaredLane() {
            try {
                var data = await GetSessionData();
                var laneArray = from player in data.MyTeam where player.SummonerId == summonerID select player.AssignedPosition;
                if (laneArray.Count() <= 0) return "NA";
                var lane = laneArray.Single();
                if (string.IsNullOrEmpty(lane)) return "NA";
                return lane;
            } catch (Exception error) {
                throw error;
            }
        }


        public async Task<bool> WaitForTheFinalPhase() {
            bool final = false;
            while (!final) {
                var session = await GetSessionData();
                final = session.Timer.Phase == "FINALIZATION";
                await Task.Delay(2000);
            }
            return final;
        }
    }
}
