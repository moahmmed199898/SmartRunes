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
using NewLeagueApp.LCU.Runes;
using NewLeagueApp.LCU;
namespace NewLeagueApp {
    /// <summary>
    /// Interaction logic for RunesWindow.xaml
    /// </summary>
    public partial class RunesPage : Page {
        public RunesPage() {
            try {
                InitializeComponent();
                Init();
            } catch (Exception) {
                throw;
            }
        }

        private async void Init() {
            var smartRunes = new SmartRunes();
            var champions = new Champions();
            var lcu = new LCU.LCU();
            await champions.Init();
            await lcu.Init();
            var currentChamp = await champions.GetCurrentChamp();
            var DeclaredLane = await lcu.GetDeclaredLane();
            var enamyChamp = await champions.GetChampLanningAginst(DeclaredLane);
            currentChampPic.ImageSource = champions.GetChampImageBrush(currentChamp);
            if (enamyChamp != "NA") enamyChampPic.ImageSource = champions.GetChampImageBrush(enamyChamp);
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


        }

        private void MarkSelectedRune(ref Slot slot, int runeID) {
            for(int i = 0; i<slot.Runes.Count();i++) {
                if (slot.Runes[i].Id == runeID) slot.Runes[i].selected = true;
            }
        }

        
        private void SetupTheStatRunes(int[] runeIDs, int rowNumber) {
            var runes = new Runes();
            var tempSlot = new Slot();
            var tempRunes = new List<Rune>();
            tempSlot.Runes = tempRunes;
            for (int i = 6; i < runeIDs.Length; i++) {
                var runeID = runeIDs[i];
                var runeInfo = runes.GetRuneInfo(runeID);
                var tempRune = new Rune {
                    Id = runeInfo.id,
                    Icon = runeInfo.IconPath,
                    Name = runeInfo.name,
                    selected = true
                };
                tempSlot.Runes.Add(tempRune);
            }
            //SetupTheRunes(SecoundryRunes, rowNumber, tempSlot);
        }
        private void SetupTheRunes(Grid primaryGrid, int rowNumber, Slot runes) {
            var Grid = SetUpTheGrid(runes.Runes.Count()+1);
            primaryGrid.Children.Add(Grid);
            Grid.SetRow(Grid, rowNumber);
            var IndexCount = 0;
            foreach (var path in runes.Runes) {
                var iconPath = path.Icon;
                if (iconPath.Contains("/lol-game-data/assets/v1/")) iconPath = iconPath.Replace("/lol-game-data/assets/v1/", "");
                var image = MakeTheImage(iconPath, path.selected);
                Grid.Children.Add(image);
                Grid.SetColumn(image, IndexCount);
                IndexCount++;
            }
        }
        private Ellipse MakeTheImage(string path, bool selected) {
            var circle = new Ellipse {
                Margin = new Thickness(10),
                Stretch = Stretch.Uniform
            };
            var url = $"static/img/{path}";
            var uri = new Uri(url, UriKind.Relative);
            var bitmapImage = new BitmapImage(uri);
            ImageBrush imageBrush;
            if (!selected) {
                //make it gray 
                //https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/how-to-convert-an-image-to-greyscale
                var newFormatedBitmapSource = new FormatConvertedBitmap();
                newFormatedBitmapSource.BeginInit();
                newFormatedBitmapSource.Source = bitmapImage;
                newFormatedBitmapSource.DestinationFormat = PixelFormats.Gray32Float;
                newFormatedBitmapSource.EndInit();
                imageBrush = new ImageBrush(newFormatedBitmapSource);
            } else {
                imageBrush = new ImageBrush(bitmapImage);
            }
            circle.Fill = imageBrush;
            return circle;


        }

        private Grid SetUpTheGrid(int numberOfColumns) {
            var newGrid = new Grid();
            for(int i = 0; i<numberOfColumns;i++) {
                var column = new ColumnDefinition();
                newGrid.ColumnDefinitions.Add(column);
            }
            return newGrid;

        }
    }
}
