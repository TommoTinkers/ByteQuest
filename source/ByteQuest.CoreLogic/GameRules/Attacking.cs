using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.GameRules;

public static class Attacking
{
	public abstract record PlayerAttackInfo;

	public sealed record PlayerAttackResults(GameLedger Ledger, GameState state, ImmutableArray<PlayerAttackInfo> Info);
	public static PlayerAttackResults Attack(this PlayersTurn playersTurn, GameLedger ledger, GameState state)
	{
		var enemy = playersTurn.Enemy;

		var(entry, percentile) = state.RollPercentile();
		ledger = ledger.RecordEntry(entry);
		state = state.ApplyChange(entry);

		var requiredPercentile = (double)state.Player.Accuracy / enemy.Evasion;
		var didHit = percentile <= requiredPercentile;

		return new PlayerAttackResults(ledger, state, ImmutableArray<PlayerAttackInfo>.Empty);
	}
}