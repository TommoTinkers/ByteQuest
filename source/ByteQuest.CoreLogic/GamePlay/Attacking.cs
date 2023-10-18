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
	public static PlayerAttackResults Attack(this PlayersTurn playersTurn, StateAndLedger snl)
	{
		var enemy = playersTurn.Enemy;
		var player = snl.State.Player;
		(snl, var percentile) = snl.RollPercentile();
		

		var requiredPercentile = BattleRules.AttackSuccessPercentile(player.Accuracy,enemy.Evasion);
		var didHit = percentile >= requiredPercentile;

		return new PlayerAttackResults(snl.Ledger, snl.State, ImmutableArray<PlayerAttackInfo>.Empty);
	}
}