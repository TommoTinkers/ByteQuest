using ByteQuest.Core.Data;

namespace ByteQuest.Core.Modes;

public abstract record BattleMode(Player Player, Enemy Enemy) : Mode;

public sealed record PlayersTurn(Player Player, Enemy Enemy) : BattleMode(Player, Enemy);

public sealed record EnemiesTurn(Player Player, Enemy Enemy) : BattleMode(Player, Enemy);


