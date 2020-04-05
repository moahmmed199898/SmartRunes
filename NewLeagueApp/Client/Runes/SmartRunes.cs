using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using NewLeagueApp.Client.Types;
using System.Net.Http;
using NewLeagueApp.Client.ChampionInfo;
namespace NewLeagueApp.Client.Runes {
    public class SmartRunes:Runes {
        /// <summary>
        /// Firebase endpoint for getting the runes 
        /// </summary>
        private readonly string getRunesRESTEndPoint = "https://us-central1-leagueapp-4596d.cloudfunctions.net/getRunes";
        /// <summary>
        /// web client that will be used to get optimal runes from firebase
        /// </summary>
        private readonly WebClient webClient = new WebClient();

        private String currentChamp;
        private String enamyChamp;
        private String lane;
        private TemperRunesRESTResponse optimalRunes;
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
                var gameSession = new GameSession();
                var enamyChampFinder = new EnamyChampFinder();
                await champions.Init();
                await gameSession.Init();
                await enamyChampFinder.Init();
                Status.CurrentStatus = "waiting for you to pick a champion";
                this.currentChamp = await champions.GetCurrentChamp();
                Status.CurrentStatus = "waiting for final phase";
                await gameSession.WaitForTheFinalPhase();
                Status.CurrentStatus = "getting the lane";
                this.lane = await gameSession.GetDeclaredLane();
                Status.CurrentStatus = "getting enamy lanner";
                this.enamyChamp = await enamyChampFinder.GetChampLanningAginst(this.lane);
                Status.CurrentStatus = "setting runes";
                await SetOptimalRunes(this.lane, this.currentChamp, this.enamyChamp);
                /*
                                Console.WriteLine("init classes");
                                var champions = new Champions();
                                var gameSession = new GameSession();
                                await champions.Init();
                                await gameSession.Init();
                                Status.CurrentStatus = "waiting for the current champ";
                                this.currentChamp = "Varus";
                                Status.CurrentStatus = "waiting for final phase";
                                await gameSession.WaitForTheFinalPhase();
                                Status.CurrentStatus = "waiting for lane";
                                this.lane = "ADC";
                                Status.CurrentStatus = "waiting for lanner";
                                this.enamyChamp = await champions.GetChampLanningAginst(this.lane);
                                Status.CurrentStatus = "setting runes";
                                await SetOptimalRunes(this.lane, this.currentChamp, this.enamyChamp);*/
            } catch(HttpRequestException) {
                await Task.Delay(3000);
                await AutoRuneSetter();
            }
            catch(Exception error) {
                Console.WriteLine(error.StackTrace);
                throw error;
            }

        }


        public void SetPlayerName(String playerName) {
            this.optimalRunes.PlayerName = playerName;
        }

        public void SetCurrentChamp(String currentChamp) {
            this.currentChamp = currentChamp;
        }
        public void SetEnamyChamp(String enamyChamp) {
            this.enamyChamp = enamyChamp;
        }
        public void SetLane(String lane) {
            this.lane = lane;
        }

        public void SetOptimalItems(List<int> items) {
            if (this.optimalRunes == null) this.optimalRunes = new TemperRunesRESTResponse();
            this.optimalRunes.Items = items;
        }

        public String GetPlayerName() {
            return this.optimalRunes.PlayerName;
        }

        public String GetCurrentChamp() {
            return this.currentChamp;
        }
        public String GetEnamyChamp() {
            return this.enamyChamp;
        }
        public String GetLane() {
            return this.lane;
        }

        public List<int> GetOptimalItems() {
            return this.optimalRunes.Items;
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
                this.optimalRunes = Runes;
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
                this.optimalRunes = Runes;
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
            try {
                var jsonString = await webClient.DownloadStringTaskAsync(getParams);
                var RunesArray = JsonConvert.DeserializeObject<TemperRunesRESTResponse[]>(jsonString);
                if (RunesArray.Length == 0) throw new Exception("Unable to find a match between " + getParams);
                var Runes = RunesArray[0];
                return Runes;
            } catch (Exception error) {
                throw error;
            }
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

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
