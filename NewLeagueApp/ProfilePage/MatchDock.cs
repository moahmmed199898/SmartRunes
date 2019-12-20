﻿using System;
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
        Files file;
        String summonerName;
        List<ProfileApiCalls.GameStatsStructure_participants> player;

        public MatchDock(int matchnum, string summonerName, List<ProfileApiCalls.GameStatsStructure_participants> player)
        {

            this.summonerName = summonerName;
            file = new Files(summonerName);
            
            this.Children.Add(new Label());
            this.player = player;
            if (player[matchnum].stats.win)
            {
                Background = new SolidColorBrush(Colors.ForestGreen);
            }
            else
            {
                Background = new SolidColorBrush(Colors.LightPink);
            }
            LoadData(matchnum);
        }

        private async void LoadData(int matchnum )
        {
           
            await file.AddUnaddedMatches(5);
           //player = await file.GetSummonerStats(summonerName);
            await AddKda(player[matchnum]);
        }
        private async Task AddKda(ProfileApiCalls.GameStatsStructure_participants player)
        {
            Label kdalbl = new Label();
            kdalbl.Content = player.stats.kills + "/" + player.stats.deaths + "/" + player.stats.assists + " (" + player.stats.kda+")";
            this.Children.Add(kdalbl);
            DockPanel.SetDock(kdalbl, Dock.Top);

        }

    }
}
