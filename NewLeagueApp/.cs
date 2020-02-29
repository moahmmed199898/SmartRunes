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
        private Boolean canScroll=false;
        private List<DockPanel> matches;
        private List<DockPanel> scrollSegment;
        public Stats()
        {
            InitializeComponent();

            matches = new List<DockPanel>();
            scrollSegment = new List<DockPanel>();
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#C7DFFC");


            for (int i = 0; i<4; i++)
            {
                addMatch();

            }
           foreach(DockPanel a in matches)
            {
                HistoryPannel.Children.Add(a);
            }
            HistoryPannel.MouseEnter += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseLeave += new MouseEventHandler(mouseHover);
            HistoryPannel.MouseWheel += new MouseWheelEventHandler(mouseScroll);





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
                    HistoryPannel.Children.Remove(0);
                    //scrollSegment.Add(matches[0]);
                    //matches.RemoveAt(0);
                }
                else{
                    HistoryPannel.Children.Remove(0);
                    addMatch();
                }

            }
            else if(e.Delta > 0 && canScroll)
            {
                int temp = matches.Count - HistoryPannel.Children.Count;
                HistoryPannel.Children.Clear();
                for(int i = temp; i < l; i++)
                {
                    HistoryPannel.Children.Add(matches[i]);
                }
            }
        }
        private void addMatch(){
                 DockPanel match = new DockPanel();
                match.Height = HistoryPannel.Height / 3;
                match.Background = new SolidColorBrush(Colors.Plum);


                Label test = new Label();
                test.Content = " Test" + i;
                test.FontSize = 24;
                match.Children.Add(test);
                DockPanel.SetDock(match, Dock.Top);
                Canvas.SetTop(match, Canvas.GetTop(HistoryPannel));
                matches.Add(match);
        }
    }
}