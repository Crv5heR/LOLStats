using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RiotSharp;
using RiotSharp.Endpoints.ChampionMasteryEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.StaticDataEndpoint.ProfileIcons;
using RiotSharp.Misc;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace LOLStats.Pages
{
    public partial class SummonerPage
    {
        PaginationState pagination = new PaginationState { ItemsPerPage = 15 };
        public static string SummonerName = "CruSheR";
        public static string rank = "Challenger";
        public static string rank2 = "Challenger";
        public static string queueType = "Challenger";
        public static string queueType2 = "Challenger";
        public static long SummonerLevel = 0;
        public static long SummonerIconId = 0;
        public static string LatestVersion = "12.23.1";

        public static Region regionX;

        public static int wins = 0;
        public static int wins2 = 0;
        public static int losses = 0;
        public static int losses2 = 0;
        public static bool hotStreak = false;

        private static string RiotKey = ""; // Enter your Key from https://developer.riotgames.com/
        public static string IconURL = "";

        public static string SeralizeRegionName(string regionName) => regionName switch
        {
            "EUN" => "eun1",
            "EUW" => "euw1",
            "NA" => "na1",
            _ => "europe"
        };
        public static List<ChampionMastery> Champs;
        public static ChampionListStatic ChampionList;
        public static async Task LoadSummonerData(string summonerName, Region region)
        {
            region = region;
            var api = RiotApi.GetDevelopmentInstance(RiotKey, 5, 5);

            try
            {
                var summoner = await api.Summoner.GetSummonerByNameAsync(region, summonerName);
                var league = await api.League.GetLeagueEntriesBySummonerAsync(region, summoner.Id);
                Champs = await api.ChampionMastery.GetChampionMasteriesAsync(region, summoner.Id);
                ChampionList = await api.DataDragon.Champions.GetAllAsync(LatestVersion, Language.en_US);

                var name = summoner.Name;
                var level = summoner.Level;
                var profileIconId = summoner.ProfileIconId;

                SummonerName = name;
                SummonerLevel = level;
                SummonerIconId = profileIconId;
                IconURL = "https://ddragon.leagueoflegends.com/cdn/12.23.1/img/profileicon/%.png".Replace("%", SummonerIconId.ToString());
                

                rank = league[0].Tier+" " + league[0].Rank;
                rank2 = league[1].Tier + " " + league[1].Rank;
                queueType = league[0].QueueType;
                queueType2 = league[1].QueueType;
                wins = league[0].Wins;
                wins2 = league[1].Wins;
                losses = league[0].Losses;
                losses2 = league[1].Losses;
                hotStreak = league[0].HotStreak;

                LatestVersion = api.DataDragon.Versions.GetAllAsync().Result[0];
            }
            catch (RiotSharpException ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public string GetChampionNameById(string id)
        {
            string championName;
            ChampionList.Keys.TryGetValue(int.Parse(id), out championName);
            return championName;
        }
    }
}
