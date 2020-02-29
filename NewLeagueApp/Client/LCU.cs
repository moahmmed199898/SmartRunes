﻿using LCUSharp;
using NewLeagueApp.Client.Types;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLeagueApp.Client.Runes;
using System;
using System.Threading;
using System.Windows.Media.Imaging;

namespace NewLeagueApp.Client {
    public class LCU:RiotConnecter {
        protected long summonerID; 
        public LCU() {
            _ = Init();
        }

        public async Task Init() {
            summonerID = await GetCurrentSummnerID();
        }





        public BitmapImage GetLaneBitmap(String lane) {
            if (!(lane == "TOP" || lane == "BOT" || lane == "SUPP" || lane == "JUNGLE" || lane == "MID")) throw new Exception($"lane named {lane} does not exist");
            var path = $"pack://application:,,/static/img/lane/{lane}.png";
            var uri = new Uri(path);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;

        }
        public async Task WaitForGameToStart() {
            var stringJSON = await SendRequestToRiot(HttpMethod.Get, "/lol-champ-select-legacy/v1/session");
            while(stringJSON.Contains("error")) {
                await Task.Delay(2000);
                stringJSON = await SendRequestToRiot(HttpMethod.Get, "/lol-champ-select-legacy/v1/session");
            }
            return;
        }

        public async Task<long> GetCurrentSummnerID() {
            var jsonString = await SendRequestToRiot(HttpMethod.Get, " /lol-summoner/v1/current-summoner");
            var data = JsonConvert.DeserializeObject<SummonerInfo>(jsonString);
            return data.SummonerId;
        }


    }
}