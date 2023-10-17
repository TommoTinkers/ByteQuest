using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.GameRules;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.GamePlay;

public static class Attacking
{
	public abstract record PlayerAttackInfo;

	public sealed record PlayerAttackResults(GameLedger Ledger, GameState state, ImmutableArray<PlayerAttackInfo> Info);
	public static PlayerAttackResults Attack(this PlayersTurn playersTurn, GameLedger ledger, GameState state)
	{
		var enemy = playersTurn.Enemy;
		var player = state.Player;
		var(entry, percentile) = state.RollPercentile();
		ledger = ledger.RecordEntry(entry);
		state = state.ApplyChange(entry);

		var requiredPercentile = BattleRules.AttackSuccessPercentile(player.Accuracy,enemy.Evasion);
		var didHit = percentile >= requiredPercentile;

		return new PlayerAttackResults(ledger, state, ImmutableArray<PlayerAttackInfo>.Empty);
	}
}