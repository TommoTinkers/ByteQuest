namespace ByteQuest.CoreLogic.Events;

public abstract record Event;

public sealed record CreatePlayerEvent(uint Health, uint Strength, uint Accuracy, uint Evasion, uint Defence) : Event;



