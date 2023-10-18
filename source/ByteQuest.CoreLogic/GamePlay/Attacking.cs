using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.GameRules;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.GamePlay;

public static class Attacking
{
	public abstract record PlayerAttackInfo;

	public sealed record PlayerAttackResults(ImmutableArray<PlayerAttackInfo> Info);
	public static (StateAndLedger, PlayerAttackResults) Attack(this PlayersTurn playersTurn, StateAndLedger snl)
	{
		(snl, var percentile) = snl.RollPercentile();
		
		var enemy = playersTurn.Enemy;
		var player = snl.State.Player;		

		var requiredPercentile = BattleRules.AttackSuccessPercentile(player.Accuracy,enemy.Evasion);
		var didHit = percentile >= requiredPercentile;

		return new (snl, new PlayerAttackResults(ImmutableArray<PlayerAttackInfo>.Empty));
	}
}