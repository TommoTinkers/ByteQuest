using System.Collections.Immutable;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;

public sealed record BattleModeResult<T>(GameState state, Mode nextMode, ImmutableArray<T> Info) where T : BattleModeResultInfo;

public abstract record BattleModeResultInfo;

public abstract record AttackResult : BattleModeResultInfo;

public sealed record AttackAttempted(string EnemyName) : AttackResult;

public sealed record AttackFailed : AttackResult;
public sealed record AttackSucceeded(uint damage) : AttackResult;

public sealed record EnemyWasDefeated(string enemyName) : AttackResult;

