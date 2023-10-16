using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Data.Modes;

namespace ByteQuest.CoreLogic.State;

public sealed record GameState(Player Player, int Seed, Mode Mode);




