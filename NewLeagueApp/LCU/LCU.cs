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
            var a = new Runes();
            await a.getCurrentRunes();
            var miniRunes = new RuneInfo[] {
                a.Tree.Domination.Row0.DarkHarvest,
                a.Tree.Domination.Row1.CheapShot,
                a.Tree.Domination.Row2.EyeballCollection,
                a.Tree.Domination.Row3.IngeniousHunter,
                a.Tree.Inspiration.Row1.HextechFlashtraption,
                a.Tree.Inspiration.Row2.BiscuitDelivery,
            };
            var statRunes = new StatRunes[] {
                StatRunes.CDR,
                StatRunes.AdaptiveForce,
                StatRunes.Health
            };
            await a.add(a.Tree.Domination, a.Tree.Inspiration, miniRunes, statRunes);

        }
    }
}
