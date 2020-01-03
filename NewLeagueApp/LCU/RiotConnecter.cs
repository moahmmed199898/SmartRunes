﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LCUSharp;
using System.Net.Http;
namespace NewLeagueApp.LCU {
    public class RiotConnecter {
        /// <summary>
        /// Sends a request to League client 
        /// </summary>
        /// <param name="httpMethodType">type of request</param>
        /// <param name="url">url to the request ( do not include the base url )</param>
        protected async Task<string> SendRequestToRiot(LCUSharp.HttpMethod httpMethodType, string url) {
            try {
                var League = await LeagueClient.Connect();
                HttpResponseMessage results;
                results = await League.MakeApiRequest(httpMethodType, url);
                var data = await results.Content.ReadAsStringAsync();
                return data;
            } catch (Exception err) {
                throw err;
            }
        }

        /// <summary>
        /// Sends a request to League client 
        /// </summary>
        /// <param name="httpMethodType">type of request</param>
        /// <param name="url">url to the request ( do not include the base url )</param>
        /// <param name="data">the data that goes with the request ( for none get requests )</param>
        protected async Task<HttpResponseMessage> SendRequestToRiot(LCUSharp.HttpMethod httpMethodType, string url, object data) {
            try {
                var League = await LeagueClient.Connect();
                HttpResponseMessage results;
                results = await League.MakeApiRequest(httpMethodType, url, data);
                return results;
            } catch (Exception err) {
                throw err;
            }
}

    }
}
