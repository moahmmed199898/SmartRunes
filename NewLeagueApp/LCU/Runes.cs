using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LCUSharp;
using NewLeagueApp.LCU.Types;
using System.Net.Http;
namespace NewLeagueApp.LCU {
    class Runes {

        public string name { get; set; } = "NewLeageApp";
        public Tree Tree { get; set; }
        
        public Runes() {
            Tree = getRuneDetails();
        }
        public async Task add(RuneTree primaryKeyStone, RuneTree secondaryRuneStone, RuneInfo[] runes, StatRunes[] statRunes) {
            try {
                var primaryKeyStoneID = primaryKeyStone.id;
                var secondaryRuneStoneID = secondaryRuneStone.id;
                int[] statRunesIDs = (from statRune in statRunes select (int)statRune).ToArray();
                int[] runeIDs = (from rune in runes select rune.id).ToArray();
                runeIDs = runeIDs.Concat(statRunesIDs).ToArray();
                var page = makeRunePage(primaryKeyStoneID, secondaryRuneStoneID, runeIDs);
                await sendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch(Exception error) {
                Console.WriteLine(error);
            }
        }


        public async Task getCurrentRunes() {
            try {
                var runesString = await sendRequestToRiot(LCUSharp.HttpMethod.Get, "/lol-perks/v1/currentpage");
                var runesIDs = JsonConvert.DeserializeObject<RunesPage>(runesString);
                var runes = new List<string>();
                for (int i = 0; i < runesIDs.selectedPerkIds.Length; i++) {
                    var id = runesIDs.selectedPerkIds[i];
                    var name = getRuneName(id);
                    runes.Add(name);
                }
            } catch(Exception error) {
                Console.WriteLine(error);
            }
        }


        private string getRuneName(int id) {
            var data = File.ReadAllText("static/runeDictionaryData.json");
            var runeDictionary = JsonConvert.DeserializeObject<RuneDictionary[]>(data);
            var runeNameCollection = from rune in runeDictionary where rune.id == id select rune.name;
            if (runeNameCollection.Count() == 0) return id.ToString();
            var runeName = runeNameCollection.Single();
            return runeName;
        }


        private RunesPage makeRunePage(int primaryKeyStoneID, int secondaryRuneStoneID, int[] runeIDs) {
            var runes = new RunesPage(name, primaryKeyStoneID, secondaryRuneStoneID, runeIDs);
            return runes;
        }
        private async Task<string> sendRequestToRiot(LCUSharp.HttpMethod httpMethodType, string url) {
            var League = await LeagueClient.Connect();
            HttpResponseMessage results;
            results = await League.MakeApiRequest(httpMethodType, url);
            var data = await results.Content.ReadAsStringAsync();
            return data;
        }
        private async Task<HttpResponseMessage> sendRequestToRiot(LCUSharp.HttpMethod httpMethodType, string url, object data) {
            var League = await LeagueClient.Connect();
            HttpResponseMessage results;
            results = await League.MakeApiRequest(httpMethodType, url,data);
            return results;
        }
            private Tree getRuneDetails() {
            var file = File.ReadAllText("static/runes.json");
            var runes = JsonConvert.DeserializeObject<Tree>(file);
            return runes;
        }


    }
}
