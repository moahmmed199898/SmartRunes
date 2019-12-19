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
            //file.UpdateFile();
            //file.GetFirstMatch();
            loadmatches();
           // file.DetermineDifference();
            // file.AddUnaddedMatches(3);
            // file.GetFirstMatch();
            
          
           // file.AddUnaddedMatches(3);
            
             
    
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#C7DFFC");


            HistoryPannel.MouseEnter += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseLeave += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseWheel += new MouseWheelEventHandler(mouseScroll);

        }

        private async Task loadmatches()
        {
            //await file.DetermineDifference();
           //await file.AddUnaddedMatches(5);
         
            List<ProfileApiCalls.GameStatsStructure_participants> player = await file.GetSummonerStats(summonerName);
            foreach (ProfileApiCalls.GameStatsStructure_participants stat in player )
            {
                try
                {
                    // HistoryPannel.Children.Insert(0, createMatch(Convert.ToString(stat.mapId)));
                    //HistoryPannel.Children.Add(createMatch(stat.participantIdentities[0].player.summonerName));
                    HistoryPannel.Children.Add(createMatch(Convert.ToString (stat.stats.kills)));
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }



        
         private DockPanel createMatch(String name){
                DockPanel match = new DockPanel();
                match.Height = HistoryPannel.Height / 3;
                match.Background = new SolidColorBrush(Colors.Plum);


                Label test = new Label();
                


                test.Content = name;
                counter++;
                test.FontSize = 24;
                match.Children.Add(test);
                DockPanel.SetDock(match, Dock.Top);
                Canvas.SetTop(match, Canvas.GetTop(HistoryPannel));
                matches.Add(match);
                return match;
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
                if (HistoryPannel.Children.Count > 3)
                {
                    HistoryPannel.Children.RemoveAt((0));

                    
                }
                else{
                    this.createMatch(Convert.ToString(":D"));

                    HistoryPannel.Children.RemoveAt(0);
                    
                    
                   

                }

            }
            else if(e.Delta > 0 && canScroll)
            {
               // int temp = matches.Count - HistoryPannel.Children.Count;
               int temp = matches.IndexOf((DockPanel)HistoryPannel.Children[0])-1;
                
                if(temp > 0){
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
