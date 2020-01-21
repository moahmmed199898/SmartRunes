using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LCUSharp;
using NewLeagueApp.LCU.Types;
using System.Net.Http;
namespace NewLeagueApp.LCU.Runes {
    class Runes:RiotConnecter {
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
        public Runes() {
            Tree = GetRuneDetails();
        }

        /// <summary>
        /// Adds a rune page to the League Client Asynchronously . 
        /// <para>This will not check if the runes you set are valid so if you entered invalid runes you will end up with invalid page on the League client</para>
        /// </summary>
        /// <param name="primaryPath">The path for the primary keystone</param>
        /// <param name="secondaryPath">The path for the secondary keystone</param>
        /// <param name="runes">An array of runes that includes the primary and secondary keystones. Example: new Runes.Tree.Domination.Row0.DarkHarvest</param>
        /// <param name="statRunes">An array of stat runes (Offense, Defense, and Flex). Example: NewLeagueApp.LCU.Types.StatRunes.AdaptiveForce</param>
        public async Task Add(int primaryPathID, int secondaryPathID, int[] runes, int[] statRunesIDs) {
            try {
                var runeIDs = (int[])runes.Concat(statRunesIDs);
                var page = MakeRunePage(primaryPathID, secondaryPathID, runeIDs);
                await SendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch(Exception error) {
                Console.WriteLine(error);
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
                await SendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch (Exception error) {
                Console.WriteLine(error);
            }
        }
        /// <summary>
        /// Get the current runes from the client 
        /// </summary>
        /// <returns>A list of runes</returns>
        public async Task<int[]> GetCurrentRunes() {
            try {
                var runesString = await SendRequestToRiot(LCUSharp.HttpMethod.Get, "/lol-perks/v1/currentpage");
                var runesIDs = JsonConvert.DeserializeObject<RunesPage>(runesString);
                return runesIDs.selectedPerkIds;
            } catch(Exception error) {
                throw error;
            }
        }
        public Slot getRuneSlot(int runeID) {
            if(runeID == 5002 || runeID == 5008 || runeID == 5003 || runeID == 5005 || runeID == 5007) {
                var dictionary = GetRuneDictionary();
                var runeInfo = (from runeItem in dictionary where runeItem.id == runeID select runeItem).Single();
                var slot = new Slot();
                var runes = new List<Rune>();
                var rune = new Rune();
                rune.Id = runeInfo.id;
                rune.Icon = runeInfo.IconPath;
                rune.Name = runeInfo.name;
                runes.Add(rune);
                slot.Runes = runes;
                return slot;
            }

            var runesSlot = (from path in Tree
                            from slot in path.Slots
                            from rune in slot.Runes
                            where rune.Id == runeID
                            select slot).Single();
            return runesSlot;
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
        protected RunesPage MakeRunePage(int primaryPathID, int secondaryPathID, int[] runeIDs) {
            var runes = new RunesPage(PageName, primaryPathID, secondaryPathID, runeIDs);
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
