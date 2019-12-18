using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp


{

    public class ProfileApiCalls

    {
        private String apikey;
        private String SummonerName;



        private WebClient myWebClient = new WebClient();
        System.IO.Stream myStream = new System.IO.MemoryStream();

         public ProfileApiCalls(String SummonerName)
        {
            this.SummonerName = SummonerName;
            apikey = "RGAPI-d4495231-31c3-467a-8c5c-4ba80033d3a3";
        }

        public async  void GetMatchHistory()
        {
            SummonerClass summoner = await SummonerInfo(SummonerName);
            HistoryClass hist = await HistoryInfo(summoner.accountID);
            GameStatsStructure data = await MatchInfo(hist.matches[0].gameID);
            int playernum;
            for(int i = 0; i < 10; i++)
            {
                if(SummonerName.Equals( data.participantIdentities[i].player.summonerId))
                {
                    //playernum = data.participantIdentities[i].participantId;
                    Console.WriteLine("Kills: " + data.participants[i].stats.kills + "Deaths: " + data.participants[i]
                        .stats.deaths + " Assists: " + data.participants[i].stats.assists);
                }
            }
            Console.WriteLine();
            // HistoryClass history = await HistoryInfo(summoner.accountID);
        }


        private string APIProcsser(string callURL, string calldata)
        {
            Console.WriteLine(callURL + "|||" + calldata);
            string key = $"?api_key=" + apikey;
            string name = calldata;
            myWebClient.BaseAddress = callURL;
            myStream = myWebClient.OpenRead(name + key);
            StreamReader myReader = new StreamReader(myStream);
            string JsonString;
            JsonString = myReader.ReadToEnd();
            return JsonString;
        }



        async  Task<SummonerClass> SummonerInfo(string summonerName)
        //This class simply exists to get info about the summoner.
        {
            string request = "https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-name/";
            string JsonString = APIProcsser(request, summonerName);
            SummonerClass summoner = new SummonerClass();
            summoner = JsonConvert.DeserializeObject<SummonerClass>(JsonString);
            return summoner;
        }

        async  Task<HistoryClass> HistoryInfo(string accID)
        //This class simply exists to get info about the summoner.
        {
            string request = "https://na1.api.riotgames.com/lol/match/v4/matchlists/by-account/";
            string JsonString = APIProcsser(request, accID);
            HistoryClass history = new HistoryClass();
            history = JsonConvert.DeserializeObject<HistoryClass>(JsonString);
            return history;
        }
        async  Task<GameStatsStructure> MatchInfo(String matchIndex)
        {
            string request = "https://na1.api.riotgames.com/lol/match/v4/matches/";
            string JsonString = APIProcsser(request, matchIndex);
            GameStatsStructure data = new GameStatsStructure();
            data = JsonConvert.DeserializeObject<GameStatsStructure>(JsonString);
      
            return data;

        }




        //Structure classes 
         public class SummonerClass
        {
            [JsonProperty("profilelconid")]
             int profileicon_id { get; set; }
            [JsonProperty("name")]
             string name { get; set; }
            [JsonProperty("puuid")]
             string puu_id { get; set; }
            [JsonProperty("summonerLevel")]
             string summonerLevel { get; set; }
            [JsonProperty("revisionDate")]
             string revisionDate { get; set; }
            [JsonProperty("id")]
             string id { get; set; }
            [JsonProperty("accountID")]
             public string accountID { get; set; }
        }


        public class HistoryClass
        {
            [JsonProperty("matches")]
             public List<Match> matches { get; set; }
            [JsonProperty("totalGames")]
             int totalGames { get; set; }
            [JsonProperty("startIndex")]
             int startIndex { get; set; }
            [JsonProperty("endIndex")]
             int endIndex { get; set; }


        }

        public class Match
        {
            [JsonProperty("lane")]
             string lane { get; set; }
            [JsonProperty("gameID")]
             public string gameID { get; set; }
            [JsonProperty("champion")]
             string champion { get; set; }
            [JsonProperty("platformId")]
             string platformId { get; set; }
            [JsonProperty("timestamp")]
             string timestamp { get; set; }
            [JsonProperty("queue")]
             string queue { get; set; }
            [JsonProperty("role")]
             string role { get; set; }
            [JsonProperty("season")]
             string season { get; set; }



        }




        public class GameStatsStructure
            {
                 long gameCreation { get; set; }
                 int gameDuration { get; set; }
                 long gameId { get; set; }
                 string gameMode { get; set; }
                 string gameType { get; set; }
                 string gameVersion { get; set; }
                 int mapId { get; set; }
                 string platformId { get; set; }
                 int queueId { get; set; }
                 int seasonId { get; set; }
            [JsonProperty("participantIdentities")]
             public List<GameStatsStructure_participantIdentities> participantIdentities { get; set; }
            [JsonProperty("participants")]
                 public List<GameStatsStructure_participants> participants { get; set; }
                 List<GameStatsStructure_teams> teams { get; set; }
            }

        public class GameStatsStructure_participantIdentities
            {
           [JsonProperty("participantId")]
                 int participantId { get; set; }
            [JsonProperty("player")]
                 public GameStatsStructure_participantIdentities_player player { get; set; }
            }
        public class GameStatsStructure_participantIdentities_player
            {
                 string accountId { get; set; }
          
                 string currentAccountId { get; set; }
                 string currentPlatformId { get; set; }
                 string matchHistoryUri { get; set; }
                 string platformId { get; set; }
                 int profileIcon { get; set; }
                 public string summonerId { get; set; }
            [JsonProperty("summonerName")]
                 string summonerName { get; set; }
            }
        public class GameStatsStructure_participants
            {
                 int championId { get; set; }
                 string highestAchievedSeasonTier { get; set; }
                 int participantId { get; set; }
                 int spell1Id { get; set; }
                 int spell2Id { get; set; }
                 int teamId { get; set; }
            [JsonProperty("stats")]
             public GameStatsStructure_participants_stats stats { get; set; }
            }
        public class GameStatsStructure_participants_stats
            {
            [JsonProperty("assits")]
             public int assists { get; set; }
                 int champLevel { get; set; }
                 int combatPlayerScore { get; set; }
                 int damageDealtToObjectives { get; set; }
                 int damageDealtToTurrets { get; set; }
                 int damageSelfMitigated { get; set; }
            [JsonProperty("deaths")]
             public int deaths { get; set; }
                 int doubleKills { get; set; }
                 bool firstBloodAssist { get; set; }
                 bool firstBloodKill { get; set; }
                 bool firstInhibitorAssist { get; set; }
                 bool firstInhibitorKill { get; set; }
                 bool firstTowerAssist { get; set; }
                 bool firstTowerKill { get; set; }
                 int goldEarned { get; set; }
                 int goldSpent { get; set; }
                 int inhibitorKills { get; set; }
                 int item0 { get; set; }
                 int item1 { get; set; }
                 int item2 { get; set; }
                 int item3 { get; set; }
                 int item4 { get; set; }
                 int item5 { get; set; }
                 int item6 { get; set; }
                 int killingSprees { get; set; }
            [JsonProperty("kills")]
             public int kills { get; set; }
                 int largestCriticalStrike { get; set; }
                 int largestKillingSpree { get; set; }
                 int largestMultiKill { get; set; }
                 int longestTimeSpentLiving { get; set; }
                 int magicDamageDealt { get; set; }
                 int magicDamageDealtToChampions { get; set; }
                 int magicalDamageTaken { get; set; }
                 int neutralMinionsKilled { get; set; }
                 int neutralMinionsKilledEnemyJungle { get; set; }
                 int neutralMinionsKilledTeamJungle { get; set; }
                 int objectivePlayerScore { get; set; }
                 int participantId { get; set; }
                 int pentaKills { get; set; }
                 int perk0 { get; set; }
                 int perk0Var1 { get; set; }
                 int perk0Var2 { get; set; }
                 int perk0Var3 { get; set; }
                 int perk1 { get; set; }
                 int perk1Var1 { get; set; }
                 int perk1Var2 { get; set; }
                 int perk1Var3 { get; set; }
                 int perk2 { get; set; }
                 int perk2Var1 { get; set; }
                 int perk2Var2 { get; set; }
                 int perk2Var3 { get; set; }
                 int perk3 { get; set; }
                 int perk3Var1 { get; set; }
                 int perk3Var2 { get; set; }
                 int perk3Var3 { get; set; }
                 int perk4 { get; set; }
                 int perk4Var1 { get; set; }
                 int perk4Var2 { get; set; }
                 int perk4Var3 { get; set; }
                 int perk5 { get; set; }
                 int perk5Var1 { get; set; }
                 int perk5Var2 { get; set; }
                 int perk5Var3 { get; set; }
                 int perkPrimaryStyle { get; set; }
                 int perkSubStyle { get; set; }
                 int physicalDamageDealt { get; set; }
                 int physicalDamageDealtToChampions { get; set; }
                 int physicalDamageTaken { get; set; }
                 int playerScore0 { get; set; }
                 int playerScore1 { get; set; }
                 int playerScore2 { get; set; }
                 int playerScore3 { get; set; }
                 int playerScore4 { get; set; }
                 int playerScore5 { get; set; }
                 int playerScore6 { get; set; }
                 int playerScore7 { get; set; }
                 int playerScore8 { get; set; }
                 int playerScore9 { get; set; }
                 int quadraKills { get; set; }
                 int sightWardsBoughtInGame { get; set; }
                 int statPerk0 { get; set; }
                 int statPerk1 { get; set; }
                 int statPerk2 { get; set; }
                 int timeCCingOthers { get; set; }
                 int totalDamageDealt { get; set; }
                 int totalDamageDealtToChampions { get; set; }
                 int totalDamageTaken { get; set; }
                 int totalHeal { get; set; }
                 int totalMinionsKilled { get; set; }
                 int totalPlayerScore { get; set; }
                 int totalScoreRank { get; set; }
                 int totalTimeCrowdControlDealt { get; set; }
                 int totalUnitsHealed { get; set; }
                 int tripleKills { get; set; }
                 int trueDamageDealt { get; set; }
                 int trueDamageDealtToChampions { get; set; }
                 int trueDamageTaken { get; set; }
                 int turretKills { get; set; }
                 int unrealKills { get; set; }
                 int visionScore { get; set; }
                 int visionWardsBoughtInGame { get; set; }
                 int wardsKilled { get; set; }
                 int wardsPlaced { get; set; }
                 bool win { get; set; }
            }

             class GameStatsStructure_teams
            {
                 int baronKills { get; set; }
                 int dominionVictoryScore { get; set; }
                 int dragonKills { get; set; }
                 bool firstBaron { get; set; }
                 bool firstBlood { get; set; }
                 bool firstDragon { get; set; }
                 bool firstInhibitor { get; set; }
                 bool firstRiftHerald { get; set; }
                 bool firstTower { get; set; }
                 int inhibitorKills { get; set; }
                 int riftHeraldKills { get; set; }
                 int teamId { get; set; }
                 int towerKills { get; set; }
                 int vilemawKills { get; set; }
                 string win { get; set; }
                 List<GameStatsStructure_teams_bans> bans { get; set; }
            }
        public class GameStatsStructure_teams_bans
            {
                 int championId { get; set; }
                 int pickTurn { get; set; }
            }
        }
    }

