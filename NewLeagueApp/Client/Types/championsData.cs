using System.Collections.Generic;

namespace NewLeagueApp.Client.Types
{
    public struct ChampionData
{
    public string Type { get; set; }
    public string Format { get; set; }
    public string Version { get; set; }
    public Dictionary<string, Champion> Data { get; set; }
}

public struct Champion
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

public struct Image
{
    public string Full { get; set; }
    public string Sprite { get; set; }
    public string Group { get; set; }
    public long X { get; set; }
    public long Y { get; set; }
    public long W { get; set; }
    public long H { get; set; }
}

public struct Info
{
    public long Attack { get; set; }
    public long Defense { get; set; }
    public long Magic { get; set; }
    public long Difficulty { get; set; }
}


public enum Tag { Assassin, Fighter, Mage, Marksman, Support, Tank };

}
