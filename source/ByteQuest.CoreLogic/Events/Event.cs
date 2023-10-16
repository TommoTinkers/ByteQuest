using ByteQuest.CoreLogic.Data;

namespace ByteQuest.CoreLogic.Events;

public abstract record Event;

public sealed record GameStartedEvent(Player Player, int Seed) : Event;



