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
        
        public Stats()
        {
            InitializeComponent();


             file = new Files("Naymliss");

            //file.UpdateFile();
            file.GetFirstMatch();
           // file.DetermineDifference();
            // file.AddUnaddedMatches(3);
            // file.GetFirstMatch();
            
          
           // file.AddUnaddedMatches(3);
            
           


            matches = new List<DockPanel>();
    
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#C7DFFC");


            for (int i = 0; i<3; i++)
            {
                createMatch();

            }
           foreach(DockPanel a in matches)
            {
                HistoryPannel.Children.Add(a);
            }
            HistoryPannel.MouseEnter += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseLeave += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseWheel += new MouseWheelEventHandler(mouseScroll);

        }



        
         private DockPanel createMatch(){
                DockPanel match = new DockPanel();
                match.Height = HistoryPannel.Height / 3;
                match.Background = new SolidColorBrush(Colors.Plum);


                Label test = new Label();
                


                test.Content = " Test" + counter;
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
            }           }
        }
    }
}
