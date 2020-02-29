namespace NewLeagueApp.Client.Types {
    public partial class IRunesPage {

        public int[] autoModifiedSelections { get; private set; } = new int[] { 0 };
        public bool? current { get; private set; } = true;
        public int? id { get; private set; } = 0;
        public bool? isActive { get; private set; } = true;
        public bool? isDeletable { get; private set; } = true;
        public bool? isEditable { get; private set; } = true;
        public bool? isValid { get; private set; } = true;
        public long? lastModified { get; private set; } = 0;
        public string name { get; set; }
        public int? order { get; private set; } = 0;
        public int primaryStyleId { get; set; }
        public int[] selectedPerkIds { get; set; }
        public int subStyleId { get; set; }
        public IRunesPage(string name, int primaryRuneKeyStone, int secondaryRuneKeyStone, int[] selectedRunes)
        {
            this.name = name;
            this.primaryStyleId = primaryRuneKeyStone;
            this.subStyleId = secondaryRuneKeyStone;
            this.selectedPerkIds = selectedRunes;

        }
    }
}