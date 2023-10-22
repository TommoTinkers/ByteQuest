using System.Collections.Immutable;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;

public abstract record BattleModeResultInfo;

public abstract record AttackResult : BattleModeResultInfo;

public sealed record AttackAttempted(string EnemyName) : AttackResult;

public sealed record AttackFailed : AttackResult;
public sealed record AttackSucceeded(uint damage) : AttackResult;

public sealed record EnemyWasDefeated(string enemyName) : AttackResult;

public sealed record BattleModeResult(GameState state, Mode nextMode, ImmutableArray<BattleModeResultInfo> Info);