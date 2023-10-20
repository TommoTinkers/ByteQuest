using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.GameRules;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.GamePlay;

public static class Attacking
{
	public abstract record PlayerAttackInfo;

	public sealed record AttackAttempted : PlayerAttackInfo;

	public sealed record AttackSucceeded : PlayerAttackInfo;

	public sealed record DamageDealt(uint amount, uint remaining) : PlayerAttackInfo;

	public sealed record AttackFailed : PlayerAttackInfo;
	
	public sealed record PlayerAttackResults(ImmutableArray<PlayerAttackInfo> Info);
	public static (StateAndLedger, PlayerAttackResults) Attack(this PlayersTurn playersTurn, StateAndLedger snl)
	{
		var infos = new List<PlayerAttackInfo> { new AttackAttempted() };


		(snl, var percentile) = snl.RollPercentile();
		
		var enemy = playersTurn.Enemy;
		var player = snl.State.Player;		

		var requiredPercentile = BattleRules.AttackSuccessPercentile(player.Accuracy,enemy.Evasion);
		var didHit = percentile >= requiredPercentile;

		infos.Add(didHit ? new AttackSucceeded() : new AttackFailed());

		if (didHit)
		{
			(snl, var damageRoll) = snl.RollPercentile();
			(snl, var defenceRoll) = snl.RollPercentile();
			var damage =
				BattleRules.CalculateDamage(snl.State.Player.Strength, playersTurn.Enemy.Defence, damageRoll, defenceRoll);

			var remaining = playersTurn.Enemy.Health - damage;
			
			infos.Add(new DamageDealt(damage, remaining));

			snl = snl.RecordEntry(new DamageEnemyEntry(damage));
		}	
					
		
		return new (snl, new PlayerAttackResults(infos.ToImmutableArray()));
	}
}