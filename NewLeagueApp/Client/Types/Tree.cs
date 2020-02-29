using System.Collections.Generic;

public partial class Tree {
    public int Id { get; set; }
    public string Key { get; set; }
    public string Icon { get; set; }
    public string Name { get; set; }
    public List<Slot> Slots { get; set; }
}

public partial class Slot {
    public List<Rune> Runes { get; set; }
}

public partial class Rune {
    public int Id { get; set; }
    public string Key { get; set; } = "NA";
    public string Icon { get; set; }
    public string Name { get; set; }
    public string ShortDesc { get; set; } = "NA";
    public string LongDesc { get; set; } = "NA";
    public bool selected { get; set; } = false;
}
