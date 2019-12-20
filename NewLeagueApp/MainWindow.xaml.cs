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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewLeagueApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //TODO: add panels
    public partial class MainWindow : Window
    {

        //private MouseEventHandler handler;
        public MainWindow()
        {
           
            InitializeComponent();
            _ = new LCU.LCU();
            
            Stats.MouseLeftButtonDown+= new MouseButtonEventHandler(mouseClickedResponse);
            Stats.MouseEnter += new MouseEventHandler(mouseHover);
            
        }

        private void mouseClickedResponse(object sender, EventArgs e)
        {
            Console.WriteLine("Test");
            Stats stat = new Stats();
            stat.Height = this.Height;
            stat.Width = this.Width;
            stat.Left = this.Left;
            stat.Top = this.Top;

            stat.Show();
            this.Close();
            Console.WriteLine(":)");


        }
        private void mouseHover(object sender, EventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#C7DFFC");
            brush.Freeze();
            Stats.Background = brush;
            
        }



    }
 
}
