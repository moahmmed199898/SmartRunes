using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LCUSharp;
namespace NewLeagueApp.LCU {
    class LCU {
        public LCU()
        {
            init();
        }

        public async void init()
        {
            var League = await LeagueClient.Connect();
            var iconID = new { profileIconId =  0 };
            var icon = await League.MakeApiRequest(HttpMethod.Put, "/lol-summoner/v1/current-summoner/icon", iconID);
            Console.WriteLine(icon);
        }
    }
}
