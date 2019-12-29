using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using NewLeagueApp.LCU.Types;
namespace NewLeagueApp.LCU {
    class SmartRunes:Runes {
        private string getRunesRESTEndPoint = "https://us-central1-leagueapp-4596d.cloudfunctions.net/getRunes";
        private WebClient webClient = new WebClient();
        public SmartRunes() {
            webClient.BaseAddress = getRunesRESTEndPoint;
            init();
        }

        public async Task init() {
            try {
                var jsonString = await webClient.DownloadStringTaskAsync("?currentChamp=soNa&enemyChamp=janna&lane=supp");
                var RunesArray = JsonConvert.DeserializeObject<TemperRunesRESTResponse[]>(jsonString);
                var Runes = RunesArray[0];
                var runeIds = new int[] {
                Runes.PrimaryPathKeystoneRune,
                Runes.PrimaryPathRune1,
                Runes.PrimaryPathRune2,
                Runes.PrimaryPathRune3,
                Runes.SecondaryPathRune1,
                Runes.SecondaryPathRune2,
                Runes.StatRune1,
                Runes.StatRune2,
                Runes.StatRune3
            };
                var page = makeRunePage(Runes.PrimaryPath, Runes.SecondaryPath, runeIds);
                await sendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
                Console.WriteLine("Done");
            } catch(Exception error) {
                Console.WriteLine(error.Message);
                throw error;
            }
        }
    }
}
