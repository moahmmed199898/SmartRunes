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
using NewLeagueApp.Client.ChampionInfo;
using NewLeagueApp.Client.Runes;
using NewLeagueApp.Client;
using System.Windows.Navigation;
using System.Diagnostics;
namespace NewLeagueApp {
    /// <summary>
    /// Interaction logic for RunesWindow.xaml
    /// </summary>
    public partial class RunesPage : Page, IDisposable {
        public RunesPage(SmartRunes smartRunes) {
            try {
                InitializeComponent();
                _ = Init(smartRunes);
            } catch (Exception error) {
                MessageBox.Show(error.Message);
                Application.Current.Shutdown();
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(this);   
        }
        private async Task Init(SmartRunes smartRunes) {

            try {
                var champions = new Champions();
                await champions.Init();
                var currentChamp = smartRunes.GetCurrentChamp();
                var enamyChamp = smartRunes.GetEnamyChamp();
                var lane = smartRunes.GetLane();
                currentChampPic.ImageSource = champions.GetChampImageBrush(currentChamp);
                if (enamyChamp != "NA") enamyChampPic.ImageSource = champions.GetChampImageBrush(enamyChamp);
                laneImage.Source = LCU.GetLaneBitmap(lane);
                var runes = await smartRunes.GetCurrentRunes();
                backgroundImage.Source = champions.GetChampBackgroundImageBrush(currentChamp);
                keyStoneImage.Source = smartRunes.GetRuneBitmap(runes[0]);
                primaryPerk1.ImageSource = smartRunes.GetRuneBitmap(runes[1]);
                primaryPerk2.ImageSource = smartRunes.GetRuneBitmap(runes[2]);
                primaryPerk3.ImageSource = smartRunes.GetRuneBitmap(runes[3]);
                secoundryPerk1.ImageSource = smartRunes.GetRuneBitmap(runes[4]);
                secoundryPerk2.ImageSource = smartRunes.GetRuneBitmap(runes[5]);
                stat1.Source = smartRunes.GetRuneBitmap(runes[6]);
                stat2.Source = smartRunes.GetRuneBitmap(runes[7]);
                stat3.Source = smartRunes.GetRuneBitmap(runes[8]);
                //items
                var items = smartRunes.GetOptimalItems();
                item1.ImageSource = Items.GetItemBitmap(items[0]);
                item2.ImageSource = Items.GetItemBitmap(items[1]);
                item3.ImageSource = Items.GetItemBitmap(items[2]);
                item4.ImageSource = Items.GetItemBitmap(items[3]);
                item5.ImageSource = Items.GetItemBitmap(items[4]);
                item6.ImageSource = Items.GetItemBitmap(items[5]);
                playerNameLink.NavigateUri = new Uri("https://na.op.gg/summoner/userName="+smartRunes.GetPlayerName(), UriKind.Absolute);
                playerNameLink.Inlines.Clear();
                playerNameLink.Inlines.Add(smartRunes.GetPlayerName());
            } catch (Exception error) {
                MessageBox.Show(error.Message);
            }
        }

        private void Reset(object sender, MouseButtonEventArgs e) {
            this.Dispose();
            NavigationService.Navigate(new StartingPage());
        }

        private void openHyperLinkInExternalURL(object sender, RequestNavigateEventArgs e) {            
            var senderHyperLink = (Hyperlink)sender;
            var url = senderHyperLink.NavigateUri;
            Process.Start(url.AbsoluteUri);
            e.Handled = true;
        }
    }
}
