using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NewLeagueApp.Updater;
namespace NewLeagueApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
     partial class App : Application
    {
        public App() {
            _ = this.Init();
        }
        public async Task Init() {
            await MatchupsUpdater.CheckForUpdates();
        }
    
    }
}
