using System.Collections.Immutable;
using ByteQuest.Core.Data;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;

public abstract record BattleMode(Player Player, Enemy Enemy) : Mode;

public sealed record PlayersTurn(Player Player, Enemy Enemy) : BattleMode(Player, Enemy);

public sealed record EnemiesTurn(Player Player, Enemy Enemy) : BattleMode(Player, Enemy);

