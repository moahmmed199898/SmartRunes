using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp.Client.Types
{
    public partial class championData
{
    public string Type { get; set; }
    public string Format { get; set; }
    public string Version { get; set; }
    public Dictionary<string, Datum> Data { get; set; }
}

public partial class Datum
{
    public string Version { get; set; }
    public string Id { get; set; }
    public int Key { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Blurb { get; set; }
    public Info Info { get; set; }
    public Image Image { get; set; }
    public List<Tag> Tags { get; set; }
    public string Partype { get; set; }
    public Dictionary<string, double> Stats { get; set; }
}

public partial class Image
{
    public string Full { get; set; }
    public string Sprite { get; set; }
    public string Group { get; set; }
    public long X { get; set; }
    public long Y { get; set; }
    public long W { get; set; }
    public long H { get; set; }
}

public partial class Info
{
    public long Attack { get; set; }
    public long Defense { get; set; }
    public long Magic { get; set; }
    public long Difficulty { get; set; }
}


public enum Tag { Assassin, Fighter, Mage, Marksman, Support, Tank };

}
