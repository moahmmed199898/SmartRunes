using LCUSharp;
using NewLeagueApp.LCU.Types;
using Newtonsoft.Json;
using System;
using System.IO;
namespace NewLeagueApp.LCU {
    class LCU {
        public LCU()
        {
            init();
        }

        public async void init()
        {
            _ = new SmartRunes();

        }
    }
}
