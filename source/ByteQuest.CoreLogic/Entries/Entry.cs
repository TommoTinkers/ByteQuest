
namespace ByteQuest.CoreLogic.Entries;

public abstract record Entry(Guid id);



public sealed record UpdateSeedEntry(int newSeed, Guid id) : Entry(id);



