using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LCUSharp;
using NewLeagueApp.Client.Types;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace NewLeagueApp.Client.Runes {
    public class Runes {
        /// <summary>
        /// Rune page name
        /// </summary>
        public string PageName { get; set; } = "NewLeageApp";
        /// <summary>
        /// Rune tree which includes the name of the rune as well as the path to it's icon
        /// </summary>
        public Tree[] Tree { get; private set; }

        /// <summary>
        /// This class can send and receive rune pages from the client and contains the Tree property which has the name and the icon path of every rune ( except stat runes )
        /// </summary>
        /// 

        protected RiotConnecter riotConnecter;
        public Runes() {
            Tree = GetRuneDetails();
            this.riotConnecter = new RiotConnecter();
        }

        /// <summary>
        /// Adds a rune page to the League Client Asynchronously . 
        /// <para>This will not check if the runes you set are valid so if you entered invalid runes you will end up with invalid page on the League client</para>
        /// </summary>
        /// <param name="primaryPath">The path for the primary keystone</param>
        /// <param name="secondaryPath">The path for the secondary keystone</param>
        /// <param name="runes">An array of runes that includes the primary and secondary keystones. Example: new Runes.Tree.Domination.Row0.DarkHarvest</param>
        /// <param name="statRunes">An array of stat runes (Offense, Defense, and Flex). Example: NewLeagueApp.Client.Types.StatRunes.AdaptiveForce</param>
        public async Task Add(int primaryPathID, int secondaryPathID, int[] runes, int[] statRunesIDs) {
            try {
                var runeIDs = (int[])runes.Concat(statRunesIDs);
                var page = MakeRunePage(primaryPathID, secondaryPathID, runeIDs);
                await this.riotConnecter.SendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch(Exception error) {
                Console.WriteLine(error);
                throw error;
            }
        }

        /// <summary>
        /// Adds a rune page to the League Client Asynchronously . 
        /// <para>This will not check if the runes you set are valid so if you entered invalid runes you will end up with invalid page on the League client</para>
        /// </summary>
        /// <param name="primaryPath">The path for the primary keystone</param>
        /// <param name="secondaryPath">The path for the secondary keystone</param>
        /// <param name="runes">An array of runes that includes the primary and secondary keystones including the stat runes </param>
        public async Task Add(int primaryPathID, int secondaryPathID, int[] runes) {
            try {
                var page = MakeRunePage(primaryPathID, secondaryPathID, runes);
                await this.riotConnecter.SendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch (Exception error) {
                Console.WriteLine(error);
                throw error;
            }
        }
        /// <summary>
        /// Get the current runes from the client 
        /// </summary>
        /// <returns>A list of runes</returns>
        public async Task<int[]> GetCurrentRunes() {
            try {
                var runesString = await this.riotConnecter.SendRequestToRiot(LCUSharp.HttpMethod.Get, "/lol-perks/v1/currentpage");
                var runesIDs = JsonConvert.DeserializeObject<Types.RunesPage>(runesString);
                return runesIDs.selectedPerkIds;
            } catch(Exception error) {
                throw error;
            }
        }

        public BitmapImage GetRuneBitmap(int runeID) {
            var path = $"pack://application:,,/static/img/{GetRuneInfo(runeID).IconPath}";
            if (path.Contains("/lol-game-data/assets/v1/")) path = path.Replace("/lol-game-data/assets/v1/", "");
            var uri = new Uri(path);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;

        }

        /// <summary>
        /// Gets the rune name from it's id
        /// </summary>
        /// <param name="id">The rune id</param>
        /// <returns>The rune name</returns>
        protected string GetRuneName(int id) {

            var runeDictionary = GetRuneDictionary();
            var runeNameCollection = from rune in runeDictionary where rune.id == id select rune.name;
            if (runeNameCollection.Count() == 0) return id.ToString();
            var runeName = runeNameCollection.Single();
            return runeName;
        }

        public RuneDictionary GetRuneInfo(int id) {
            var runeDictionary = GetRuneDictionary();
            var runeNameCollection = from rune in runeDictionary where rune.id == id select rune;
            var runeName = runeNameCollection.Single();
            return runeName;
        }

        private RuneDictionary[] GetRuneDictionary() {
            var data = File.ReadAllText("static/runeDictionaryData.json");
            var runeDictionary = JsonConvert.DeserializeObject<RuneDictionary[]>(data);
            return runeDictionary;
        }

        /// <summary>
        /// Makes a rune page that can be send to League client
        /// </summary>
        /// <param name="primaryPathID">The primary path id</param>
        /// <param name="secondaryPathID">The secondary path id</param>
        /// <param name="runeIDs">The runes id ( does not include stat runes )</param>
        /// <returns>A rune page</returns>
        protected Types.RunesPage MakeRunePage(int primaryPathID, int secondaryPathID, int[] runeIDs) {
            var runes = new Types.RunesPage(PageName, primaryPathID, secondaryPathID, runeIDs);
            return runes;
        }
        /// <summary>
        /// Gets more detailed information about the runes 
        /// </summary>
        /// <returns>A more detailed information about the runes</returns>
        protected Tree[] GetRuneDetails() {
        var file = File.ReadAllText("static/runes.json");
        var runes = JsonConvert.DeserializeObject<Tree[]>(file);
        return runes;
    }


    }
}
