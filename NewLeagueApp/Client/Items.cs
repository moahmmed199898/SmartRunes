using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NewLeagueApp.Client {
    class Items {
        public static BitmapImage GetItemBitmap(int itemID) {
            var path = $"pack://application:,,/static/img/item/{itemID}.png";
            if (path.Contains("/lol-game-data/assets/v1/")) path = path.Replace("/lol-game-data/assets/v1/", "");
            var uri = new Uri(path);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;

        }
    }
}
