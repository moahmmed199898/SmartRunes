using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace NewLeagueApp.ProfilePage
{


    //this dock pulls from files to get match data

   
    class MatchDock : DockPanel
    {
       
        private String summonerName;
        private List<ProfileApiCalls.GameStatsStructure_participants> player;
        private MatchStats matchstat;

        public MatchDock(int matchnum, string summonerName, List<ProfileApiCalls.GameStatsStructure_participants> player, MatchStats matchstat)
        {

            this.summonerName = summonerName;
            this.matchstat = matchstat;
            
            this.Children.Add(new Label());
            this.player = player;
            if (player[matchnum].stats.win)
            {
                var converter = new System.Windows.Media.BrushConverter();
                Background = ((Brush)converter.ConvertFromString("#95bda1"));
            }
            else
            {
                var converter = new System.Windows.Media.BrushConverter();
                Background = ((Brush)converter.ConvertFromString("#a87979"));
            }
            LoadData(matchnum);
        }

        private async void LoadData(int matchnum )
        {
           
           // await file.AddUnaddedMatches(5);
           //player = await file.GetSummonerStats(summonerName);
            await AddKda(player[matchnum]);
            await AddDPM();
            await AddCSM();
        }
        private async Task AddKda(ProfileApiCalls.GameStatsStructure_participants player)
        {
            Label kdalbl = new Label();
            kdalbl.Content = player.stats.kills + "/" + player.stats.deaths + "/" + player.stats.assists + " (" + player.stats.kda+")";
            this.Children.Add(kdalbl);
            DockPanel.SetDock(kdalbl, Dock.Top);
        }
        private async Task AddDPM()
        {
            Label label = new Label();
  
            double damage = await matchstat.getDPM();
            label.Content = "Damage Per Minute: " + damage;
            Children.Add(label);
        }
        private async Task AddCSM(){
            Label label = new Label();
            double cs = await matchstat.getCSM();
            label.Content = "CS/Min: " + cs;
            Children.Add(label);
        }



    }
}
