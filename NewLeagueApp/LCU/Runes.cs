﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LCUSharp;
using NewLeagueApp.LCU.Types;
using System.Net.Http;
namespace NewLeagueApp.LCU {
    class Runes {
        /// <summary>
        /// Rune page name
        /// </summary>
        public string name { get; set; } = "NewLeageApp";
        /// <summary>
        /// Rune tree which includes the name of the rune as well as the path to it's icon
        /// </summary>
        public Tree Tree { get; private set; }
        /// <summary>
        /// This class can send and receive rune pages from the client and contains the Tree property which has the name and the icon path of every rune ( except stat runes )
        /// </summary>
        public Runes() {
            Tree = getRuneDetails();
        }

        /// <summary>
        /// Adds a rune page to the League Client Asynchronously . 
        /// <para>This will not check if the runes you set are valid so if you entered invalid runes you will end up with invalid page on the League client</para>
        /// <para>
        /// Example:
        /// <code>
        ///    //init the runes
        ///    var a = new Runes();
        ///    //make the runes array
        ///    var miniRunes = new RuneInfo[] {
        ///            a.Tree.Domination.Row0.DarkHarvest,
        ///            a.Tree.Domination.Row1.CheapShot,
        ///            a.Tree.Domination.Row2.EyeballCollection,
        ///            a.Tree.Domination.Row3.IngeniousHunter,
        ///            a.Tree.Inspiration.Row1.HextechFlashtraption,
        ///            a.Tree.Inspiration.Row2.BiscuitDelivery,
        ///        };
        ///    //make the stat runes array
        ///    var statRunes = new StatRunes[] {
        ///            StatRunes.CDR,
        ///            StatRunes.AdaptiveForce,
        ///            StatRunes.Health
        ///        };
        ///    //send the runes to the client
        ///    await a.add(a.Tree.Domination, a.Tree.Inspiration, miniRunes, statRunes);
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="primaryPath">The path for the primary keystone</param>
        /// <param name="secondaryPath">The path for the secondary keystone</param>
        /// <param name="runes">An array of runes that includes the primary and secondary keystones. Example: new Runes.Tree.Domination.Row0.DarkHarvest</param>
        /// <param name="statRunes">An array of stat runes (Offense, Defense, and Flex). Example: NewLeagueApp.LCU.Types.StatRunes.AdaptiveForce</param>
        public async Task add(RuneTree primaryPath, RuneTree secondaryPath, RuneInfo[] runes, StatRunes[] statRunes) {
            try {
                var primaryKeyStoneID = primaryPath.id;
                var secondaryRuneStoneID = secondaryPath.id;
                int[] statRunesIDs = (from statRune in statRunes select (int)statRune).ToArray();
                int[] runeIDs = (from rune in runes select rune.id).ToArray();
                runeIDs = runeIDs.Concat(statRunesIDs).ToArray();
                var page = makeRunePage(primaryKeyStoneID, secondaryRuneStoneID, runeIDs);
                await sendRequestToRiot(LCUSharp.HttpMethod.Put, "/lol-perks/v1/pages/1701818929", page);
            } catch(Exception error) {
                Console.WriteLine(error);
            }
        }

        /// <summary>
        /// Get the current runes from the client 
        /// </summary>
        /// <returns></returns>
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


        protected string getRuneName(int id) {
            var data = File.ReadAllText("static/runeDictionaryData.json");
            var runeDictionary = JsonConvert.DeserializeObject<RuneDictionary[]>(data);
            var runeNameCollection = from rune in runeDictionary where rune.id == id select rune.name;
            if (runeNameCollection.Count() == 0) return id.ToString();
            var runeName = runeNameCollection.Single();
            return runeName;
        }


        protected RunesPage makeRunePage(int primaryKeyStoneID, int secondaryRuneStoneID, int[] runeIDs) {
            var runes = new RunesPage(name, primaryKeyStoneID, secondaryRuneStoneID, runeIDs);
            return runes;
        }
        protected async Task<string> sendRequestToRiot(LCUSharp.HttpMethod httpMethodType, string url) {
            var League = await LeagueClient.Connect();
            HttpResponseMessage results;
            results = await League.MakeApiRequest(httpMethodType, url);
            var data = await results.Content.ReadAsStringAsync();
            return data;
        }
        protected async Task<HttpResponseMessage> sendRequestToRiot(LCUSharp.HttpMethod httpMethodType, string url, object data) {
            var League = await LeagueClient.Connect();
            HttpResponseMessage results;
            results = await League.MakeApiRequest(httpMethodType, url,data);
            return results;
        }
        protected Tree getRuneDetails() {
        var file = File.ReadAllText("static/runes.json");
        var runes = JsonConvert.DeserializeObject<Tree>(file);
        return runes;
    }


    }
}
