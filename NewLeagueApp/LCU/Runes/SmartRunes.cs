using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using NewLeagueApp.LCU.Types;
using System.Net.Http;

namespace NewLeagueApp.LCU.Runes {
    class SmartRunes:Runes {
        /// <summary>
        /// Firebase endpoint for getting the runes 
        /// </summary>
        private readonly string getRunesRESTEndPoint = "https://us-central1-leagueapp-4596d.cloudfunctions.net/getRunes";
        /// <summary>
        /// web client that will be used to get optimal runes from firebase
        /// </summary>
        private readonly WebClient webClient = new WebClient();
        /// <summary>
        /// This class will get the optimal runes based on the game status and set them up in the League client 
        /// </summary>
        public SmartRunes() {
            webClient.BaseAddress = getRunesRESTEndPoint;
        }
        /// <summary>
        /// This will automate the rune setting process 
        /// </summary>
        public async Task AutoRuneSetter() {
            try {
                Console.WriteLine("init classes");
                var champions = new Champions();
                var lcu = new LCU();
                await champions.Init();
                await lcu.init();
                Console.WriteLine("wait for the current Champ");
                var currentChamp = await champions.GetCurrentChamp();
                Console.WriteLine("wait for final phase");
                await lcu.WaitForTheFinalPhase();
                Console.WriteLine("wait for lane");
                var lane = await lcu.GetDeclaredLane();
                Console.WriteLine("wait for lanner");
                var enamyChamp = await champions.GetChampLanningAginst(lane);
                Console.WriteLine("setting runes");
                await SetOptimalRunes(lane, currentChamp, enamyChamp);
            } catch(HttpRequestException error) {
                Thread.Sleep(3000);
                await AutoRuneSetter();
            }
            catch(Exception error) {
                Console.WriteLine(error.StackTrace);
                throw error;
            }

        }    
        /// <summary>
        /// set the optimal runes based on the champ selected and lane
        /// </summary>
        /// <param name="lane">The lane the player is playing in</param>
        /// <param name="currentChamp">The champ the player selected</param>
        public async Task SetOptimalRunes(string lane, string currentChamp) {
            try {
                var Runes = await RunesGetRequest($"?currentChamp={currentChamp}&enemyChamp=NA&lane={lane}");
                var runeIds = GetRunesIDs(Runes);
                var page = MakeRunePage(Runes.PrimaryPath, Runes.SecondaryPath, runeIds);
                await SendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch(Exception error) {
                Console.WriteLine(error.Message);
                throw error;
            }
        }
        /// <summary>
        /// set the optimal runes based on the champ selected, the champ you are laning against, and the lane the player is playing in. 
        /// </summary>
        /// <param name="lane">The lane the player is playing in</param>
        /// <param name="currentChamp">The champ the player selected</param>
        /// <param name="enamyChamp">The champ the player is laning  against</param>
        public async Task SetOptimalRunes(string lane, string currentChamp, string enamyChamp) {
            try {
                var Runes = await RunesGetRequest($"?currentChamp={currentChamp}&enemyChamp={enamyChamp}&lane={lane}");
                var runeIds = GetRunesIDs(Runes);
                var page = MakeRunePage(Runes.PrimaryPath, Runes.SecondaryPath, runeIds);
                await SendRequestToRiot(LCUSharp.HttpMethod.Delete, "/lol-perks/v1/pages");
                await SendRequestToRiot(LCUSharp.HttpMethod.Post, "/lol-perks/v1/pages", page);
            } catch (Exception error) {
                throw error;
            }
        }
        /// <summary>
        /// Get the runes from firebase
        /// </summary>
        /// <param name="getParams">the get params for the request</param>
        /// <returns>The runes from firebase</returns>
        private async Task<TemperRunesRESTResponse> RunesGetRequest(string getParams) {
            var jsonString = await webClient.DownloadStringTaskAsync(getParams);
            var RunesArray = JsonConvert.DeserializeObject<TemperRunesRESTResponse[]>(jsonString);
            var Runes = RunesArray[0];
            return Runes;
        }
        /// <summary>
        /// Convert runes objects (TemperRunesRESTResponse) to an int[] resembling runes ids
        /// </summary>
        /// <param name="runes">the runes object from firebase</param>
        /// <returns>an array of runes ids</returns>
        private int[] GetRunesIDs(TemperRunesRESTResponse runes) {

            var runeIds = new int[] {
                runes.PrimaryPathKeystoneRune,
                runes.PrimaryPathRune1,
                runes.PrimaryPathRune2,
                runes.PrimaryPathRune3,
                runes.SecondaryPathRune1,
                runes.SecondaryPathRune2,
                runes.StatRune1,
                runes.StatRune2,
                runes.StatRune3
            };
            return runeIds;
        }
    }
}
