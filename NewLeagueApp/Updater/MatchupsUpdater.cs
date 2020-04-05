using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using NewLeagueApp.Client.Types;

namespace NewLeagueApp.Updater {
    public class MatchupsUpdater {

        private static readonly int updateCycle = 3;
        public async static Task CheckForUpdates() {
            //check if file exists 
            if (!File.Exists("cache/champMatchUps.json")) await Update();
            //check when was the last time the file got updated 
            var lastUpdated = File.GetLastWriteTime("cache/champMatchUps.json");
            if((DateTime.Now - lastUpdated).TotalDays >= updateCycle) {
                await Update();
            }
        }


        private static async Task Update() {
            var webClient = new WebClient();
            webClient.BaseAddress = "https://us-central1-leagueapp-4596d.cloudfunctions.net/getChampMatchups";
            var jsonString = await webClient.DownloadStringTaskAsync("");
            Directory.CreateDirectory("cache");
            StreamWriter file = File.CreateText("cache/champMatchUps.json");
            file.WriteLine(jsonString);
            file.Close();
        }
    }


}
