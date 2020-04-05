using System;
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
namespace NewLeagueApp.Client.ChampionInfo {
    public class Champions: Champion {
        /// <summary>
        /// Deals with everything regarding champions. 
        /// <para>
        /// When you initialize this class make sure you call Init() or you will not be able to use championsInformation property 
        /// </para>
        /// </summary>
        private String currentChamp;

        public Champions() {
        }
        
        /// <summary>
        /// (Do Not Use)
        /// Gets the current champion from the client or throws "No Active Game" if no game is active yet 
        /// </summary>
        public async Task<string> GetCurrentChamp() {
            try {
                if (this.currentChamp != null) return this.currentChamp;
                //var currentChampIDString = await SendRequestToRiot(LCUSharp.HttpMethod.Get, "lol-champ-select/v1/current-champion");
                var IcurrentChampID = from player in this.gameSession.MyTeam where player.SummonerId == summonerID select player.ChampionId;
                if (IcurrentChampID.Count()==0) { await Task.Delay(2000); return await GetCurrentChamp(); };
                var currentChampID = IcurrentChampID.Single<int>();
                if (currentChampID == 0) { await Task.Delay(2000); return await GetCurrentChamp(); };
                var currentChampName = (from champ in championsInformation.Data where champ.Value.Key == currentChampID select champ.Value.Name).Single();
                this.currentChamp = currentChampName;
                return currentChampName;
            } catch(Exception error) {
                throw error;
            }
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
