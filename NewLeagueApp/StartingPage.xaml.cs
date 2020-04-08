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
using NewLeagueApp.Client.Runes;
using System.Threading;
namespace NewLeagueApp {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class StartingPage : Page, IDisposable {
        public StartingPage() {
            InitializeComponent();
            _ = UpdateTheStatus();
            _ = Init();
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }

        private async Task Init() {
            try {
                var smartRunes = new SmartRunes();
                Logger.Log("waiting for the AutoRuneSetter");
                Status.CurrentStatus = "Waiting for the game to start";
                await smartRunes.AutoRuneGenerator();
                Logger.Log("Runes Arrived going to the RunesPage");
                NavigationService.Navigate(new RunesPage(smartRunes));
                Status.window.Visibility = Visibility.Visible;
                Dispose();
            } catch (Exception error) {
                Console.WriteLine(error.StackTrace);
                Logger.Log($"Error: {error.Message}");
                Logger.Log($"Error Location: StartingPage.xaml.cs");
                Logger.Log($"Error Stack Trace: {error.StackTrace}");
                MessageBox.Show(error.Message);
                Application.Current.Shutdown();
            }
        }
        private async Task UpdateTheStatus() {
            try {
                while(true) {
                    statusLabel.Content = Status.CurrentStatus;
                    await Task.Delay(2000);
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
