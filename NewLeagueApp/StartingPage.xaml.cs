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
using NewLeagueApp.LCU.Runes;
using System.Threading;
namespace NewLeagueApp {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class StartingPage : Page {
        public StartingPage() {
            InitializeComponent();
            updateTheStatus();
            Init();
        }

        private async void Init() {
            try {
                var smartRunes = new SmartRunes();
                await smartRunes.AutoRuneSetter();
                NavigationService.Navigate(new RunesPage());
            } catch (Exception) {
                throw;
            }
        }
        private async void updateTheStatus() {
            try {
                while(true) {
                    statusLabel.Content = Status.currentStatus;
                    await Task.Delay(2000);
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
