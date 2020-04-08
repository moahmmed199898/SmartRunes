using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLeagueApp.Client.Types;
using Newtonsoft.Json;
using NewLeagueApp.Client;
using NewLeagueApp.Tools;
using System.Windows.Media.Imaging;

namespace NewLeagueApp.Client.ChampionInfo {

    public class Champion {
        /// <summary>
        /// Contains info about the champions which includes their stats ( damage,armor, ...etc) info about their story, image, and more
        /// </summary>
        protected ChampionData championsInformation;
        protected sessionData gameSessionData;
        protected GameSession gameSession;


        /// <summary>
        /// initialize the class and set championsInformation property 
        /// </summary>
        public async Task Init() {
            this.championsInformation = await GetChampInfo();
            this.gameSession = new GameSession();
            this.gameSessionData = await this.gameSession.GetSessionData();
        }

        /// <summary>
        /// gets the champ info from the json file
        /// </summary>
        /// <returns>The champ info as championData</returns>
        private async Task<ChampionData> GetChampInfo() {
            var jsonstring = await AsyncIO.ReadFileAsync("static/champion.json");
            return JsonConvert.DeserializeObject<ChampionData>(jsonstring);
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
            var pathQuery = (from champ in championsInformation.Data where champ.Value.Id == name select champ.Value.Image.Full);
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


    }
}
