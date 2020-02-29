using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NewLeagueApp {
    class Logger {
        private static readonly StreamWriter file = File.AppendText("SmartRunes.log");
        public static void Log(string message) {
            String dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            file.WriteLine($"{dateTime} : {message}");
        }
    }
}
