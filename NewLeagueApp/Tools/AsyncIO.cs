using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp.Tools {
    public class AsyncIO {

        /// <summary>
        /// reads a file asynchronously 
        /// </summary>
        /// <param name="fileName">The file name to be read</param>
        /// <returns>the file data</returns>
        public static async Task<string> ReadFileAsync(string fileName) {
            try {
                Encoding enc = Encoding.GetEncoding("iso-8859-1");
                var file = File.OpenRead(fileName);
                var returnBuffer = new byte[file.Length];
                await file.ReadAsync(returnBuffer, 0, (int)file.Length);
                return enc.GetString(returnBuffer);
            } catch (Exception error) {
                throw error;
            }

        }
    }
}
