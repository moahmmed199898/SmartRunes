using System.Collections.Generic;

public struct  Tree {
    public int Id { get; set; }
    public string Key { get; set; }
    public string Icon { get; set; }
    public string Name { get; set; }
    public List<Slot> Slots { get; set; }
}

public struct  Slot {
    public List<Rune> Runes { get; set; }
}

public struct  Rune {
    public int Id;
    public string Key;
    public string Icon;
    public string Name;
    public string ShortDesc;
    public string LongDesc;
}
