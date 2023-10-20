
namespace ByteQuest.CoreLogic.Entries;

public abstract record Entry;



public sealed record UpdateSeedEntry(int newSeed) : Entry;


public abstract record UpdateModeEntry : Entry ;



