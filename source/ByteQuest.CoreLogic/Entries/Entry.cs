
namespace ByteQuest.CoreLogic.Entries;

public abstract record Entry;



public sealed record UpdateSeedEntry(int newSeed) : Entry;


public abstract record UpdateModeEntry : Entry ;

public abstract record BattleModeEntry : UpdateModeEntry;

public sealed record DamageEnemyEntry(uint Amount) : BattleModeEntry;

