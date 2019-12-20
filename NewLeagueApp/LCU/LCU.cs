using LCUSharp;
using NewLeagueApp.LCU.Types;
using Newtonsoft.Json;
using System;
namespace NewLeagueApp.LCU {
    class LCU {
        public LCU()
        {
            init();
        }

        public async void init()
        {
            var League = await LeagueClient.Connect();
            var iconID = new { profileIconId = 0 };
            var icon = await League.MakeApiRequest(HttpMethod.Put, "/lol-summoner/v1/current-summoner/icon", iconID);
            var runes = new RunesPages("Test2", 8000, 8200, new int[] { 8008, 9111, 9105, 8017, 8232, 8226, 5008, 5008, 5001 });


            var pageID = await League.MakeApiRequest(HttpMethod.Put, "/lol-perks/v1/pages/1701818929", runes);
            Console.WriteLine(JsonConvert.SerializeObject(runes));

            var a = new Rune();
            a.Domination.DominationRow1.CheapShot.id = 0;
        }
    }
}
