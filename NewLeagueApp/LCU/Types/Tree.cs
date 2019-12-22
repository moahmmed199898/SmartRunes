using Newtonsoft.Json;
public class RuneInfo {
    public int id { get; set; }
    public string key { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public string shortDesc { get; set; }
    public string longDesc { get; set; }
}


public class RuneTree {
    public int id { get; set; }
    public string icon { get; set; }
    public string name { get; set; }

}






public class DominationRow0 {
    public RuneInfo Electrocute { get; set; }
    public RuneInfo Predator { get; set; }
    public RuneInfo DarkHarvest { get; set; }
    public RuneInfo HailOfBlades { get; set; }
}





public class DominationRow1 {
    public RuneInfo CheapShot { get; set; }
    public RuneInfo TasteOfBlood { get; set; }
    public RuneInfo SuddenImpact { get; set; }
}




public class DominationRow2 {
    public RuneInfo ZombieWard { get; set; }
    public RuneInfo GhostPoro { get; set; }
    public RuneInfo EyeballCollection { get; set; }
}

public class DominationRow3 {
    public RuneInfo RavenousHunter { get; set; }
    public RuneInfo IngeniousHunter { get; set; }
    public RuneInfo RelentlessHunter { get; set; }
    public RuneInfo UltimateHunter { get; set; }
}

public class Domination:RuneTree {
    
    [JsonProperty("DominationRow0")]
    public DominationRow0 Row0 { get; set; }
    [JsonProperty("DominationRow1")]
    public DominationRow1 Row1 { get; set; }
    [JsonProperty("DominationRow2")]
    public DominationRow2 Row2 { get; set; }
    [JsonProperty("DominationRow3")]
    public DominationRow3 Row3 { get; set; }
}

public class InspirationRow0 {
    public RuneInfo GlacialAugment { get; set; }
    public RuneInfo UnsealedSpellbook { get; set; }
    public RuneInfo MasterKey { get; set; }
}

public class InspirationRow1 {
    public RuneInfo HextechFlashtraption { get; set; }
    public RuneInfo MagicalFootwear { get; set; }
    public RuneInfo PerfectTiming { get; set; }
}

public class InspirationRow2 {
    public RuneInfo FuturesMarket { get; set; }
    public RuneInfo MinionDematerializer { get; set; }
    public RuneInfo BiscuitDelivery { get; set; }
}

public class InspirationRow3 {
    public RuneInfo CosmicInsight { get; set; }
    public RuneInfo ApproachVelocity { get; set; }
    public RuneInfo TimeWarpTonic { get; set; }
}

public class Inspiration:RuneTree {
    [JsonProperty("InspirationRow0")]
    public InspirationRow0 Row0 { get; set; }
    [JsonProperty("InspirationRow1")]
    public InspirationRow1 Row1 { get; set; }
    [JsonProperty("InspirationRow2")]
    public InspirationRow2 Row2 { get; set; }
    [JsonProperty("InspirationRow3")]
    public InspirationRow3 Row3 { get; set; }
}

public class PrecisionRow0 {
    public RuneInfo PressTheAttack { get; set; }
    public RuneInfo LethalTempo { get; set; }
    public RuneInfo FleetFootwork { get; set; }
    public RuneInfo Conqueror { get; set; }
}

public class PrecisionRow1 {
    public RuneInfo Overheal { get; set; }
    public RuneInfo Triumph { get; set; }
    public RuneInfo PresenceOfMind { get; set; }
}
public class PrecisionRow2 {
    public RuneInfo LegendAlacrity { get; set; }
    public RuneInfo LegendTenacity { get; set; }
    public RuneInfo LegendBloodline { get; set; }
}

public class PrecisionRow3 {
    public RuneInfo CoupDeGrace { get; set; }
    public RuneInfo CutDown { get; set; }
    public RuneInfo LastStand { get; set; }
}

public class Precision:RuneTree {
    [JsonProperty("PrecisionRow0")]
    public PrecisionRow0 Row0 { get; set; }
    [JsonProperty("PrecisionRow1")]
    public PrecisionRow1 Row1 { get; set; }
    [JsonProperty("PrecisionRow2")]
    public PrecisionRow2 Row2 { get; set; }
    [JsonProperty("PrecisionRow3")]
    public PrecisionRow3 Row3 { get; set; }
}

public class ResolveRow0 {
    public RuneInfo GraspOfTheUndying { get; set; }
    public RuneInfo Aftershock { get; set; }
    public RuneInfo Guardian { get; set; }
}

public class ResolveRow1 {
    public RuneInfo Demolish { get; set; }
    public RuneInfo FontOfLife { get; set; }
    public RuneInfo ShieldBash { get; set; }
}

public class ResolveRow2 {
    public RuneInfo Conditioning { get; set; }
    public RuneInfo SecondWind { get; set; }
    public RuneInfo BonePlating { get; set; }
}

public class ResolveRow3 {
    public RuneInfo Overgrowth { get; set; }
    public RuneInfo Revitalize { get; set; }
    public RuneInfo Unflinching { get; set; }
}

public class Resolve:RuneTree {
    [JsonProperty("ResolveRow0")]
    public ResolveRow0 Row0 { get; set; }
    [JsonProperty("ResolveRow1")]
    public ResolveRow1 Row1 { get; set; }
    [JsonProperty("ResolveRow2")]
    public ResolveRow2 Row2 { get; set; }
    [JsonProperty("ResolveRow3")]
    public ResolveRow3 Row3 { get; set; }
}



public class SorceryRow0 {
    public RuneInfo SummonAery { get; set; }
    public RuneInfo ArcaneComet { get; set; }
    public RuneInfo PhaseRush { get; set; }
}

public class SorceryRow1 {
    public RuneInfo NullifyingOrb { get; set; }
    public RuneInfo ManaflowBand { get; set; }
    public RuneInfo NimbusCloak { get; set; }
}

public class SorceryRow2 {
    public RuneInfo Transcendence { get; set; }
    public RuneInfo Celerity { get; set; }
    public RuneInfo AbsoluteFocus { get; set; }
}

public class SorceryRow3 {
    public RuneInfo Scorch { get; set; }
    public RuneInfo Waterwalking { get; set; }
    public RuneInfo GatheringStorm { get; set; }
}

public class Sorcery:RuneTree {
    [JsonProperty("SorceryRow0")]
    public SorceryRow0 Row0 { get; set; }
    [JsonProperty("SorceryRow1")]
    public SorceryRow1 Row1 { get; set; }
    [JsonProperty("SorceryRow2")]
    public SorceryRow2 Row2 { get; set; }
    [JsonProperty("SorceryRow3")]
    public SorceryRow3 Row3 { get; set; }
}

public class Tree {
    public Domination Domination { get; set; }
    public Inspiration Inspiration { get; set; }
    public Precision Precision { get; set; }
    public Resolve Resolve { get; set; }
    public Sorcery Sorcery { get; set; }
}
