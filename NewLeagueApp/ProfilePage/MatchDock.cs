using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NewLeagueApp.ProfilePage
{


    //this dock pulls from files to get match data

   
    class MatchDock : DockPanel
    {
        Files file;
        String summonerName;
        public MatchDock(int matchnum, string summonerName)
        {
            this.summonerName = summonerName;
            file = new Files(summonerName);
            LoadData(matchnum);
            this.Children.Add(new Label());
        }

        private async void LoadData(int matchnum)
        {
           List<ProfileApiCalls.GameStatsStructure_participants> player = await file.GetSummonerStats(summonerName);
            await AddKda(player[matchnum]);
        }
        private async Task AddKda(ProfileApiCalls.GameStatsStructure_participants player)
        {
            Label kdalbl = new Label();
            kdalbl.Content = player.stats.kills + "/" + player.stats.deaths + "/" + player.stats.assists;
            this.Children.Add(kdalbl);
            DockPanel.SetDock(kdalbl, Dock.Top);

        }

    }
}
