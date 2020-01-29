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
#pragma warning disable CS0169 // The field 'Stats.name' is never used
        private List<Label> name;
#pragma warning restore CS0169 // The field 'Stats.name' is never used
#pragma warning disable CS0169 // The field 'Stats.kda' is never used
        private List<Label> kda;
#pragma warning restore CS0169 // The field 'Stats.kda' is never used
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

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            loadmatches();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.

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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private async void AddMatches()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
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
