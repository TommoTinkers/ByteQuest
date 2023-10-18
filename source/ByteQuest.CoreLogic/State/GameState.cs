using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Ledgers;

namespace ByteQuest.CoreLogic.State;

public sealed record GameState(Player Player, int Seed, Mode Mode);


