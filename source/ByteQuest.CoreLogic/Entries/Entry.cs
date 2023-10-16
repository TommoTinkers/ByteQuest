
namespace ByteQuest.CoreLogic.Entries;

public abstract record Entry(Guid id);



public sealed record UpdateSeed(int newSeed, Guid id) : Entry(id);



