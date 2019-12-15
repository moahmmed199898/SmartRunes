using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace LeagueApp
{
    class ApiCalls
    {

        private WebClient myWebClient = new WebClient();
        System.IO.Stream myStream = new System.IO.MemoryStream();

        async public Task<string> BaseApiClass(string callURL, string calldata)
        {
            await RateLimiter.ReadyForRequest();
            return await Task.FromResult<string>(APIProcsser(callURL, calldata));
        }
        private string APIProcsser(string callURL, string calldata) {
            Console.WriteLine(callURL + "|||" + calldata);
            string key = $"?api_key={Environment.env.APIKey}";
            string name = calldata;
            myWebClient.BaseAddress = callURL;
            myStream = myWebClient.OpenRead(name + key);
            StreamReader myReader = new StreamReader(myStream);
            string JsonString;
            JsonString = myReader.ReadToEnd();
            return JsonString;
        }


        public HistoryClass matchData;
        public Boolean firstRun = true;


        async public Task<SummonerClass> SummonerInfo(string summonerName)
        //This class simply exists to get info about the summoner.
        {
            string[] results = new string[4];
            string request = "https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-name/";
            string JsonString = await BaseApiClass(request, summonerName);
            SummonerClass summoner = new SummonerClass();
            summoner = JsonConvert.DeserializeObject<SummonerClass>(JsonString);
            return summoner;
        }

        async public Task<HistoryClass> MatchInfo(string summonerName)
        {
            var summonerInfo = await SummonerInfo(summonerName);
            string[] results = new string[1000];
            string request = "https://na1.api.riotgames.com/lol/match/v4/matchlists/by-account/";
            String JsonString = await BaseApiClass(request, summonerInfo.accountID);
            HistoryClass history = new HistoryClass();
            //dynamic test = JObject.Parse(JsonString);            
            history = JsonConvert.DeserializeObject<HistoryClass>(JsonString);
            return history;
        }

        async public Task<GameStatsStructure> GameStats(uint gameID) {
            var JsonString = await BaseApiClass("https://na1.api.riotgames.com//lol/match/v4/matches/", gameID.ToString());
            var Data = JsonConvert.DeserializeObject<GameStatsStructure>(JsonString);
            return Data;
        }
    }





    //Structure classes 
    public class SummonerClass
    {
        [JsonProperty("profilelconid")]
        public int profileicon_id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("puuid")]
        public string puu_id { get; set; }
        [JsonProperty("summonerLevel")]
        public string summonerLevel { get; set; }
        [JsonProperty("revisionDate")]
        public string revisionDate { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("accountID")]
        public string accountID { get; set; }
    }
             
        
    public class HistoryClass
    {
        [JsonProperty("matches")]
        public List<Match> matches { get; set; }
        [JsonProperty("totalGames")]
        public int totalGames { get; set; }
        [JsonProperty("startIndex")]
        public int startIndex { get; set; }
        [JsonProperty("endIndex")]
        public int endIndex { get; set; }


    }

    public class Match
    {
        [JsonProperty("lane")]
        public string lane { get; set; }
        [JsonProperty("gameID")]
        public string gameID { get; set; }
        [JsonProperty("champion")]
        public string champion { get; set; }
        [JsonProperty("platformId")]
        public string platformId { get; set; }
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }
        [JsonProperty("queue")]
        public string queue { get; set; }
        [JsonProperty("role")]
        public string role { get; set; }
        [JsonProperty("season")]
        public string season { get; set; }

    }



    public class GameStatsStructure {
        public long  gameCreation { get; set; }
        public int gameDuration { get; set; }
        public long gameId { get; set; }
        public string gameMode { get; set; }
        public string gameType { get; set; }
        public string gameVersion { get; set; }
        public int mapId { get; set; }
        public string platformId { get; set; }
        public int queueId { get; set; }
        public int seasonId { get; set; }
        public List<GameStatsStructure_participantIdentities> participantIdentities { get; set; }
        public List<GameStatsStructure_participants> participants { get; set; }
        public List<GameStatsStructure_teams> teams { get; set; }
    }

    public class GameStatsStructure_participantIdentities {
        public int participantId { get; set; }
        public GameStatsStructure_participantIdentities_player player { get; set; }
    }
    public class GameStatsStructure_participantIdentities_player {
        public string accountId { get; set; }
        public string currentAccountId { get; set; }
        public string currentPlatformId { get; set; }
        public string matchHistoryUri { get; set; }
        public string platformId { get; set; }
        public int profileIcon { get; set; }
        public string summonerId { get; set; }
        public string summonerName { get; set; }
    }
    public class GameStatsStructure_participants {
        public int championId { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public int participantId { get; set; }
        public int spell1Id { get; set; }
        public int spell2Id { get; set; }
        public int teamId { get; set; }
        public GameStatsStructure_participants_stats stats { get; set; }
    }
    public class GameStatsStructure_participants_stats {
        public int assists { get; set; }
        public int champLevel { get; set; }
        public int combatPlayerScore { get; set; }
        public int damageDealtToObjectives { get; set; }
        public int damageDealtToTurrets { get; set; }
        public int damageSelfMitigated { get; set; }
        public int deaths { get; set; }
        public int doubleKills { get; set; }
        public bool firstBloodAssist { get; set; }
        public bool firstBloodKill { get; set; }
        public bool firstInhibitorAssist { get; set; }
        public bool firstInhibitorKill { get; set; }
        public bool firstTowerAssist { get; set; }
        public bool firstTowerKill { get; set; }
        public int goldEarned { get; set; }
        public int goldSpent { get; set; }
        public int inhibitorKills { get; set; }
        public int item0 { get; set; }
        public int item1 { get; set; }
        public int item2 { get; set; }
        public int item3 { get; set; }
        public int item4 { get; set; }
        public int item5 { get; set; }
        public int item6 { get; set; }
        public int killingSprees { get; set; }
        public int kills { get; set; }
        public int largestCriticalStrike { get; set; }
        public int largestKillingSpree { get; set; }
        public int largestMultiKill { get; set; }
        public int longestTimeSpentLiving { get; set; }
        public int magicDamageDealt { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int magicalDamageTaken { get; set; }
        public int neutralMinionsKilled { get; set; }
        public int neutralMinionsKilledEnemyJungle { get; set; }
        public int neutralMinionsKilledTeamJungle { get; set; }
        public int objectivePlayerScore { get; set; }
        public int participantId { get; set; }
        public int pentaKills { get; set; }
        public int perk0 { get; set; }
        public int perk0Var1 { get; set; }
        public int perk0Var2 { get; set; }
        public int perk0Var3 { get; set; }
        public int perk1 { get; set; }
        public int perk1Var1 { get; set; }
        public int perk1Var2 { get; set; }
        public int perk1Var3 { get; set; }
        public int perk2 { get; set; }
        public int perk2Var1 { get; set; }
        public int perk2Var2 { get; set; }
        public int perk2Var3 { get; set; }
        public int perk3 { get; set; }
        public int perk3Var1 { get; set; }
        public int perk3Var2 { get; set; }
        public int perk3Var3 { get; set; }
        public int perk4 { get; set; }
        public int perk4Var1 { get; set; }
        public int perk4Var2 { get; set; }
        public int perk4Var3 { get; set; }
        public int perk5 { get; set; }
        public int perk5Var1 { get; set; }
        public int perk5Var2 { get; set; }
        public int perk5Var3 { get; set; }
        public int perkPrimaryStyle { get; set; }
        public int perkSubStyle { get; set; }
        public int physicalDamageDealt { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int physicalDamageTaken { get; set; }
        public int playerScore0 { get; set; }
        public int playerScore1 { get; set; }
        public int playerScore2 { get; set; }
        public int playerScore3 { get; set; }
        public int playerScore4 { get; set; }
        public int playerScore5 { get; set; }
        public int playerScore6 { get; set; }
        public int playerScore7 { get; set; }
        public int playerScore8 { get; set; }
        public int playerScore9 { get; set; }
        public int quadraKills { get; set; }
        public int sightWardsBoughtInGame { get; set; }
        public int statPerk0 { get; set; }
        public int statPerk1 { get; set; }
        public int statPerk2 { get; set; }
        public int timeCCingOthers { get; set; }
        public int totalDamageDealt { get; set; }
        public int totalDamageDealtToChampions { get; set; }
        public int totalDamageTaken { get; set; }
        public int totalHeal { get; set; }
        public int totalMinionsKilled { get; set; }
        public int totalPlayerScore { get; set; }
        public int totalScoreRank { get; set; }
        public int totalTimeCrowdControlDealt { get; set; }
        public int totalUnitsHealed { get; set; }
        public int tripleKills { get; set; }
        public int trueDamageDealt { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int trueDamageTaken { get; set; }
        public int turretKills { get; set; }
        public int unrealKills { get; set; }
        public int visionScore { get; set; }
        public int visionWardsBoughtInGame { get; set; }
        public int wardsKilled { get; set; }
        public int wardsPlaced { get; set; }
        public bool win { get; set; }
    }

    public class GameStatsStructure_teams {
        public int baronKills { get; set; }
        public int dominionVictoryScore { get; set; }
        public int dragonKills { get; set; }
        public bool firstBaron { get; set; }
        public bool firstBlood { get; set; }
        public bool firstDragon { get; set; }
        public bool firstInhibitor { get; set; }
        public bool firstRiftHerald { get; set; }
        public bool firstTower { get; set; }
        public int inhibitorKills { get; set; }
        public int riftHeraldKills { get; set; }
        public int teamId { get; set; }
        public int towerKills { get; set; }
        public int vilemawKills { get; set; }
        public string win { get; set; }
        public List<GameStatsStructure_teams_bans> bans { get; set; }
    }
    public class GameStatsStructure_teams_bans {
        public int championId { get; set; }
        public int pickTurn { get; set; }
    }
}

 

