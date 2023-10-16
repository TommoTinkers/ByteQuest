namespace ByteQuest.CoreLogic.Data.Modes;

public abstract record Mode;

public abstract record BattleMode(Enemy Enemy) : Mode;

public sealed record PlayersTurn(Enemy Enemy) : BattleMode(Enemy);
public sealed record EnemysTurn(Enemy Enemy) : BattleMode(Enemy);