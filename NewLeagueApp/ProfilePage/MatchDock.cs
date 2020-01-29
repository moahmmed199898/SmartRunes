using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

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

            
            
            //fix this to run one time only. It increases load times currently.


            //TODO: invert text color
            System.Drawing.Image imageBackground = System.Drawing.Image.FromFile("Anivia_0.jpg");


            var converter = new System.Windows.Media.BrushConverter();
            System.Drawing.Image  imageOverlayWin = System.Drawing.Image.FromFile("Win.png");

            System.Drawing.Image imageOverlayLoss = System.Drawing.Image.FromFile("Loss.png");




            

            if (player[matchnum].stats.win)
            {
                System.Drawing.Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
                using (Graphics gr = Graphics.FromImage(img))
                {
                    gr.DrawImage(imageBackground, new Point(0, 0));
                    imageOverlayWin = (new Bitmap(imageOverlayWin, new Size(imageBackground.Width, imageBackground.Height)));
                    gr.DrawImage(imageOverlayWin, new Point(0, 0));
                }
                if (System.IO.File.Exists("outputWin.png"))
                    System.IO.File.Delete("outputWin.png");
                img.Save("outputWin.png", ImageFormat.Png);
                img.Dispose();
            }
            else if(!player[matchnum].stats.win)
            {
                System.Drawing.Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
                using (Graphics gr = Graphics.FromImage(img))
                {
                    gr.DrawImage(imageBackground, new Point(0, 0));
                    imageOverlayLoss =  (new Bitmap(imageOverlayLoss, new Size(imageBackground.Width, imageBackground.Height)));
                    gr.DrawImage(imageOverlayLoss, new Point(0, 0));
                }
                if (System.IO.File.Exists("outputLoss.png"))
                    System.IO.File.Delete("outputLoss.png");
                img.Save("outputLoss.png", ImageFormat.Png);
                img.Dispose();
            }
            

            try
            {
                if (player[matchnum].stats.win)
                {
                    var convert = new System.Windows.Media.ImageBrush();
                    convert.ImageSource = new BitmapImage(new Uri("outputWin.png", UriKind.Relative));
                    this.Background = (System.Windows.Media.Brush)convert;
                }
                else
                {
                    var convert = new System.Windows.Media.ImageBrush();
                    convert.ImageSource = new BitmapImage(new Uri("outputLoss.png", UriKind.Relative));
                    this.Background = (System.Windows.Media.Brush)convert;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            LoadData(matchnum);
        }

        private async void LoadData(int matchnum )
        {

            // await file.AddUnaddedMatches(5);
            //player = await file.GetSummonerStats(summonerName);
            Console.WriteLine(player[matchnum].championId);
            await AddKda(player[matchnum]);
            await AddDPM();
            await AddCSM();
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private async Task AddKda(ProfileApiCalls.GameStatsStructure_participants player)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
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
