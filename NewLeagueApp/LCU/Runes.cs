using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LCUSharp;
using NewLeagueApp.LCU.Types;
namespace NewLeagueApp.LCU {
    class Runes {

        public string name { get; set; } = "NewLeageApp";
        public Rune Tree { get; set; }

        public Runes() {
            Tree = getRuneDetails();
        }
        public async Task add(RuneTree primaryKeyStone, RuneTree secondaryRuneStone, RuneInfo[] runes) {
            try {
                var primaryKeyStoneID = primaryKeyStone.id;
                var secondaryRuneStoneID = secondaryRuneStone.id;
                int[] runeIDs = (from rune in runes select rune.id).ToArray<int>();
                var page = makeRunePage(primaryKeyStoneID, secondaryRuneStoneID, runeIDs);
                await sendPageToRiot(page);
            } catch(Exception error) {
                throw error;
            }
        }

        private RunesPage makeRunePage(int primaryKeyStoneID, int secondaryRuneStoneID, int[] runeIDs) {
            var runes = new RunesPage(name, primaryKeyStoneID, secondaryRuneStoneID, runeIDs);
            return runes;
        }
        private async Task sendPageToRiot(RunesPage page) {
            var League = await LeagueClient.Connect();
            var results = await League.MakeApiRequest(HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
        }

        private Rune getRuneDetails() {
            var file = File.ReadAllText("static/runes.json");
            var runes = JsonConvert.DeserializeObject<Rune>(file);
            return runes;
        }


    }
}
