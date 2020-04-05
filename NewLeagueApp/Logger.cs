using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NewLeagueApp {
    class Logger {
        public static void Log(string message) {
            StreamWriter file = File.AppendText("SmartRunes.log");
            String dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            file.WriteLine($"{dateTime} : {message}");
            file.Close();
        }

        public static void LogSessionData(string sessionString) {
            StreamWriter file = File.AppendText("SessionData.log");
            String dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            file.WriteLine($"{dateTime} : \n=========================\n {sessionString}");
            file.Close();
        }
    }
}
