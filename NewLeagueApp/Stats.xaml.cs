using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using NewLeagueApp.ProfilePage;

namespace NewLeagueApp
{


    
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        private int counter = 0;
        private Boolean canScroll=false;
        private List<DockPanel> matches;
        private Files file;
        private List<Label> name;
        private List<Label> kda;
        private String summonerName;
        
        public Stats()
        {
            InitializeComponent();
            summonerName = "Naymliss";

             file = new Files(summonerName);
            matches = new List<DockPanel>();
           // file.UpdateFile();
            //file.GetFirstMatch();
            
           //file.DetermineDifference();
            // file.AddUnaddedMatches(3);
            // file.GetFirstMatch();
            
          
           // file.AddUnaddedMatches(3);
            
             
    
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#C7DFFC");


            HistoryPannel.MouseEnter += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseLeave += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseWheel += new MouseWheelEventHandler(mouseScroll);

            loadmatches();

        }

        private async Task loadmatches()
        {

            List<ProfileApiCalls.GameStatsStructure_participants> player = await file.GetSummonerStats(summonerName);
            for (int i = 0; i < player.Count; i++)
            {
                MatchDock matchd = new MatchDock(i, summonerName,player, new MatchStats(file.stats[i],player[i].participantId-1));
                matches.Add(matchd);
                matchd.Height = (HistoryPannel.Height / 3);
                matchd.Width = HistoryPannel.Width;
                DockPanel.SetDock(matchd, Dock.Top);
               
            }
            for(int i = 0; i < 3; i++){
                 HistoryPannel.Children.Add(matches[i]);
            }

        }

        private async void AddMatches()
        {
            
   

        }




        private void mouseHover(object sender, EventArgs e)
        {
            if (canScroll == false)
            {
                canScroll = true;
                Console.WriteLine(canScroll);
            }
            else
            {
                canScroll = false;
                Console.WriteLine(canScroll);
            }
        }
        private void mouseScroll(object sender, MouseWheelEventArgs e)
        {  
            if (e.Delta < 0 && canScroll)
            {

                int temp = matches.IndexOf((DockPanel)HistoryPannel.Children[HistoryPannel.Children.Count - 1]);
                if (HistoryPannel.Children.Count < matches.Count&&counter < matches.Count-1&& temp+1 < matches.Count-1) {

                    //int temp = matches.IndexOf((DockPanel)HistoryPannel.Children[HistoryPannel.Children.Count - 1]);
                    HistoryPannel.Children.RemoveAt(0);
                    HistoryPannel.Children.Add(matches[temp + 1]);
                    
                }
                else{
                    AddMatches();

                }
                


            }
            else if(e.Delta > 0 && canScroll)
            {
               // int temp = matches.Count - HistoryPannel.Children.Count;
               int temp = matches.IndexOf((DockPanel)HistoryPannel.Children[0])-1;
                
                if(temp >= 0){
                HistoryPannel.Children.Clear();
                for(int i = temp; i < temp+3; i++)
                {
                    HistoryPannel.Children.Add(matches[i]);
                                      

                }
            }
            }
        }
    }
}
