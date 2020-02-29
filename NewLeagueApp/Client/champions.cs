﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.Client.Types;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
namespace NewLeagueApp.Client {
    public class Champions:GameSession {
        private readonly GameSession gameSession;
        /// <summary>
        /// Contains info about the champions which includes their stats ( damage,armor, ...etc) info about their story, image, and more
        /// </summary>
        public championData championsInformation;
        /// <summary>
        /// Deals with everything regarding champions. 
        /// <para>
        /// When you initialize this class make sure you call Init() or you will not be able to use championsInformation property 
        /// </para>
        /// </summary>
        private String currentChamp;

        public Champions() {
            gameSession = new GameSession();
        }
        /// <summary>
        /// initialize the class and set championsInformation property 
        /// </summary>
        public new async Task Init() {
            championsInformation = await GetChampInfo();
        }
        /// <summary>
        /// (Do Not Use)
        /// Gets the current champion from the client or throws "No Active Game" if no game is active yet 
        /// </summary>
        public async Task<string> GetCurrentChamp() {
            try {
                if (this.currentChamp != null) return this.currentChamp;
                var currentChampIDString = await SendRequestToRiot(LCUSharp.HttpMethod.Get, "lol-champ-select/v1/current-champion");
                if (currentChampIDString.Contains("\"httpStatus\":404,\"")) { await Task.Delay(2000); return await GetCurrentChamp(); };
                var currentChampID = int.Parse(currentChampIDString);
                if (currentChampID == 0) { await Task.Delay(2000); return await GetCurrentChamp(); };
                var currentChampName = (from champ in championsInformation.Data where champ.Value.Key == currentChampID select champ.Value.Name).Single();
                this.currentChamp = currentChampName;
                return currentChampName;
            } catch(Exception error) {
                throw error;
            }
        }
        /// <summary>
        /// gets the champ name from the id
        /// </summary>
        /// <param name="id">the id of the champ</param>
        /// <returns>the champ name</returns>
        public string GetChampNameById(int id) {
            string champName = (from champ in championsInformation.Data where champ.Value.Key == id select champ.Value.Name).Single();
            return champName;
        }

        public BitmapImage GetChampImageBrush(string name) {
            var path = GetChampImagePath(name);
            var uri = new Uri(path);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public BitmapImage GetChampBackgroundImageBrush(string name) {
            name = name.Replace(" ", "");
            name = name.Replace(".", "");
            name = name.Replace("'", "");
            var path = $"pack://application:,,/static/img/splash/{name}_0.jpg";
            var uri = new Uri(path);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;
        }
        /// <summary>
        /// gets the champ image path from the champ name
        /// </summary>
        /// <param name="name">the name of the champ ( not case sensitive)</param>
        /// <returns>the image path</returns>
        public string GetChampImagePath(string name) {
            var pathQuery = (from champ in championsInformation.Data where champ.Value.Name == name select champ.Value.Image.Full);
            if (pathQuery.Count() == 0) return $"pack://application:,,/static/img/champion/CHEST_187.png";
            var path = $"pack://application:,,/static/img/champion/{pathQuery.Single()}";
            return path;
        }
        /// <summary>
        /// get the champ image path from the id 
        /// </summary>
        /// <param name="id">the id of the champ</param>
        /// <returns>the image path</returns>
        public string GetChampImagePath(int id) {
            string champName = GetChampNameById(id);
            var path = $"static/img/splash/{champName}_0.jpg";
            return path;
        }
        public async Task<string[]> GetEnamyChamps() {
            try {
                var data = await gameSession.GetSessionData();
                if (data.TheirTeam == null || data.TheirTeam.Count() == 0 || data.TheirTeam[0].ChampionId == 0) {
                    string[] errorArray = { "NA" };
                    return errorArray;
                };
                var enamyChampIds = (from player in data.TheirTeam select player.ChampionId).ToArray();
                var enamyChampNames = GetChampNamesByIDs(enamyChampIds);
                return enamyChampNames;
            } catch(Exception error) {
                throw error;
            }
            throw new WarningException("Using a static file to get draft pick information");
        }

        public string[] GetChampNamesByIDs(int[] champIDs) {
            Console.WriteLine("GetChampNamesByIDs");
            var champNames = new List<string>();
            foreach(var champId in champIDs) {
                champNames.Add(GetChampNameById(champId));
            }
            return champNames.ToArray();
        }
        public async Task<string> GetChampLanningAginst(string[] enamyChamps, string myLane) {
            foreach(var enamyChamp in enamyChamps) {
                var lane = await GetChampLane(enamyChamp);
                if (lane == myLane) return enamyChamp;
            }
            return "NA";
        }
        public async Task<string> GetChampLanningAginst(string myLane) {
            var enamyChamps = await GetEnamyChamps();
            if (enamyChamps[0] == "NA") return "NA";
            foreach (var enamyChamp in enamyChamps) {
                var lane = await GetChampLane(enamyChamp);
                if (lane == myLane) return enamyChamp;
            }
            return "NA";
        }

        public async Task<string> GetChampLane(string champName) {
            try {
                var jsonString = await ReadFileAsync("static/champMatchUps.json");
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
            } catch(Exception error) {
                throw error;
            }
        }

        /// <summary>
        /// gets the champ info from the json file
        /// </summary>
        /// <returns>The champ info as championData</returns>
        private async Task<championData> GetChampInfo() {
            var jsonstring = await ReadFileAsync("static/champion.json");
            return JsonConvert.DeserializeObject<championData>(jsonstring);
        }

        /// <summary>
        /// reads a file asynchronously 
        /// </summary>
        /// <param name="fileName">The file name to be read</param>
        /// <returns>the file data</returns>
        private async Task<string> ReadFileAsync(string fileName) {
            try {
                Encoding enc = Encoding.GetEncoding("iso-8859-1");
                var file = File.OpenRead(fileName);
                var returnBuffer = new byte[file.Length];
                await file.ReadAsync(returnBuffer, 0, (int)file.Length);
                return enc.GetString(returnBuffer);
            }  catch(Exception error) {
                throw error;
            }

        }
    }
}