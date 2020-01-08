using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.LCU.Types;
using Newtonsoft.Json;
namespace NewLeagueApp.LCU {
    class Champions:RiotConnecter {
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
        public Champions() {
            
        }
        /// <summary>
        /// initialize the class and set championsInformation property 
        /// </summary>
        public async Task Init() {
            championsInformation = await GetChampInfo();
        }
        /// <summary>
        /// (Do Not Use)
        /// Gets the current champion from the client or throws "No Active Game" if no game is active yet 
        /// </summary>
        public async Task<string> GetCurrentChamp() {
            var currentChampIDString = await SendRequestToRiot(LCUSharp.HttpMethod.Get, "lol-champ-select/v1/current-champion");
            if (currentChampIDString.Contains("\"httpStatus\":404,\"")) throw new Exception("No Active Game");
            var currentChampID = int.Parse(currentChampIDString);
            var currentChampName = (from champ in championsInformation.Data where champ.Value.Key == currentChampID select champ.Value.Name).Single();
            return currentChampName;
        }

        /// <summary>
        /// gets the champ name from the id
        /// </summary>
        /// <param name="id">the id of the champ</param>
        /// <returns>the champ name</returns>
        public string GetChampNameById(int id) {
            string champName = (from champ in championsInformation.Data where champ.Value.Key == id select champ.Key).Single();
            return champName;
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

        /// <summary>
        /// gets the champ image path from the champ name
        /// </summary>
        /// <param name="name">the name of the champ ( not case sensitive)</param>
        /// <returns>the image path</returns>
        public string GetChampImagePath(string name) {
            var path = $"static/img/splash/{name}_0.jpg";
            return path;
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
            var file = File.OpenRead(fileName);
            var returnBuffer = new byte[file.Length];
            await file.ReadAsync(returnBuffer, 0, (int)file.Length);
            return Encoding.ASCII.GetString(returnBuffer);


        }
    }
}
