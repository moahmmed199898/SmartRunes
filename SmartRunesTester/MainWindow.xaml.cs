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
using NewLeagueApp;
using System.Threading;
using NewLeagueApp.Client.Runes;

namespace SmartRunesTester {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            string[] champNames = new string[] { "Sett", "Shaco", "Shen", "Shyvana", "Singed", "Sion", "Sivir", "Skarner", "Sona", "Soraka", "Swain", "Sylas", "Syndra", "TahmKench", "Taliyah", "Talon", "Taric", "Teemo", "Thresh", "Tristana", "Trundle", "Tryndamere", "TwistedFate", "Twitch", "Udyr", "Urgot", "Varus", "Vayne", "Veigar", "Velkoz", "Vi", "Viktor", "Vladimir", "Volibear", "Warwick", "Xayah", "Xerath", "XinZhao", "Yasuo", "Yorick", "Yuumi", "Zac", "Zed", "Ziggs", "Zilean", "Zoe", "Zyra" };
            InitializeComponent();
            //TestChampions(champNames);
            TestASpecifcChamp("Galio");
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private async void TestChampions() {
            SmartRunes smartRunes;
            string[] champNames = new string[] { "Aatrox", "Ahri", "Akali", "Alistar", "Amumu", "Anivia", "Annie", "Aphelios", "Ashe", "AurelionSol", "Azir", "Bard", "Blitzcrank", "Brand", "Braum", "Caitlyn", "Camille", "Cassiopeia", "Chogath", "Corki", "Darius", "Diana", "Draven", "DrMundo", "Ekko", "Elise", "Evelynn", "Ezreal", "Fiddlesticks", "Fiora", "Fizz", "Galio", "Gangplank", "Garen", "Gnar", "Gragas", "Graves", "Hecarim", "Heimerdinger", "Illaoi", "Irelia", "Ivern", "Janna", "JarvanIV", "Jax", "Jayce", "Jhin", "Jinx", "Kaisa", "Kalista", "Karma", "Karthus", "Kassadin", "Katarina", "Kayle", "Kayn", "Kennen", "Khazix", "Kindred", "Kled", "KogMaw", "Leblanc", "LeeSin", "Leona", "Lissandra", "Lucian", "Lulu", "Lux", "Malphite", "Malzahar", "Maokai", "MasterYi", "MissFortune", "MonkeyKing", "Mordekaiser", "Morgana", "Nami", "Nasus", "Nautilus", "Neeko", "Nidalee", "Nocturne", "Nunu", "Olaf", "Orianna", "Ornn", "Pantheon", "Poppy", "Pyke", "Qiyana", "Quinn", "Rakan", "Rammus", "RekSai", "Renekton", "Rengar", "Riven", "Rumble", "Ryze", "Sejuani", "Senna", "Sett", "Shaco", "Shen", "Shyvana", "Singed", "Sion", "Sivir", "Skarner", "Sona", "Soraka", "Swain", "Sylas", "Syndra", "TahmKench", "Taliyah", "Talon", "Taric", "Teemo", "Thresh", "Tristana", "Trundle", "Tryndamere", "TwistedFate", "Twitch", "Udyr", "Urgot", "Varus", "Vayne", "Veigar", "Velkoz", "Vi", "Viktor", "Vladimir", "Volibear", "Warwick", "Xayah", "Xerath", "XinZhao", "Yasuo", "Yorick", "Yuumi", "Zac", "Zed", "Ziggs", "Zilean", "Zoe", "Zyra" };
            foreach (var champName in champNames) {
                smartRunes = new SmartRunes();
                smartRunes.SetCurrentChamp(champName);
                smartRunes.SetEnamyChamp("Akali");
                smartRunes.SetLane("MID");
                smartRunes.SetOptimalItems(new System.Collections.Generic.List<int> { 1004, 1006, 1011, 1001, 1001, 1001 });
                smartRunes.SetPlayerName("Hello");
                var page = new RunesPage(smartRunes);
                Main.NavigationService.Navigate(page);
                Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                Console.WriteLine(champName);
                await Task.Delay(2000);
            }
            
          }

        private async void TestChampions(String[] champNames) {
            SmartRunes smartRunes;
            foreach (var champName in champNames) {
                smartRunes = new SmartRunes();
                smartRunes.SetCurrentChamp(champName);
                smartRunes.SetEnamyChamp("Akali");
                smartRunes.SetLane("MID");
                smartRunes.SetOptimalItems(new System.Collections.Generic.List<int> { 1004, 1006, 1011, 1001, 1001, 1001 });
                smartRunes.SetPlayerName("Hello");
                var page = new RunesPage(smartRunes);
                Main.NavigationService.Navigate(page);
                Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                Console.WriteLine(champName);
                await Task.Delay(2000);
            }

        }
        private void TestASpecifcChamp(String champName) {
                SmartRunes smartRunes;
                smartRunes = new SmartRunes();
                smartRunes.SetCurrentChamp(champName);
                smartRunes.SetEnamyChamp("Akali");
                smartRunes.SetLane("MID");
                smartRunes.SetOptimalItems(new System.Collections.Generic.List<int> { 1004, 1006, 1011, 1001, 1001, 1001 });
                smartRunes.SetPlayerName("Hello");
                var page = new RunesPage(smartRunes);
                Main.NavigationService.Navigate(page);
                Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                Console.WriteLine(champName);
            }
    }


    }


            