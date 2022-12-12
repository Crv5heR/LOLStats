using Microsoft.AspNetCore.Http.Extensions;
using RiotSharp.Misc;

namespace LOLStats.Pages
{
    public partial class Index
    {
        public string summonerName { get; set; }
        public Region selectedRegion { get; set; }
        public async Task SearchPlayer(string summonerName, Region region)
        {
            try
            {
                await SummonerPage.LoadSummonerData(summonerName, region);
                Navigation.NavigateTo("summoner");
            } catch(Exception e)
            {
                
            }
        }
    }
}
