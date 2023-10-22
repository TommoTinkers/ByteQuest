using System.Collections.Immutable;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;

public sealed record BattleModeResult<T>(GameState state, Mode nextMode, ImmutableArray<T> Info) where T : BattleModeResultInfo;

public abstract record BattleModeResultInfo;

public abstract record PlayerTurnResult : BattleModeResultInfo;

public sealed record PlayerTurnAttempted(string EnemyName) : PlayerTurnResult;

public sealed record PlayerTurnFailed : PlayerTurnResult;
public sealed record PlayerTurnSucceeded(uint damage) : PlayerTurnResult;

public sealed record EnemyWasDefeated(string enemyName) : PlayerTurnResult;

