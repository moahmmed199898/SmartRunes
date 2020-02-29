﻿using LCUSharp;
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
            var stringJSON = await SendRequestToRiot(HttpMethod.Get, "/lol-champ-select/v1/session");
            stringJSON = stringJSON.Replace("UTILITY", "SUPP");
            var data = JsonConvert.DeserializeObject<sessionData>(stringJSON);
            return data;
        }
        public async Task<string> GetDeclaredLane() {
            try {
                var data = await GetSessionData();
                data.MyTeam.ForEach(team => {
                    if (team.SummonerId == summonerID) {
                        team.AssignedPosition = "TOP";
                    }
                });
                var laneArray = from player in data.MyTeam where player.SummonerId == summonerID select player.AssignedPosition;
                if (laneArray.Count() <= 0) return "NA";
                var lane = laneArray.Single();
                if (string.IsNullOrEmpty(lane)) return "NA";
                if (lane == "SUPP") return "UTILITY";
                return lane;
            } catch (Exception error) {
                throw error;
            }
            throw new WarningException("Using a static file to get draft pick information");
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