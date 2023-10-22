using System.Collections.Immutable;
using ByteQuest.Core.Rules;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;

public abstract record EnemyAttackResult : BattleModeResultInfo;

public sealed record EnemyAttemptsToAttack(string EnemyName) : EnemyAttackResult;

public sealed record EnemySucceededInAttackingPlayer(uint Damage) : EnemyAttackResult;

public sealed record EnemyFailedInAttackingPlayer() : EnemyAttackResult;

public sealed record EnemyDefeatedPlayer() : EnemyAttackResult;

public class EnemiesTurnHandler
{
	public static BattleModeResult<EnemyAttackResult> Attack(GameState state, EnemiesTurn mode)
	{
		var info = new List<EnemyAttackResult>();
		var seed = state.Seed;
		var player = mode.Player;
		var enemy = mode.Enemy;

		
		info.Add(new EnemyAttemptsToAttack(enemy.Name));
		
		(var accuracyRoll, var evasionRoll, seed) = Rolling.RollPercentilePair(seed);
				

		var didHit = BattleCalculations.CalculateDidHit(enemy.Accuracy, player.Evasion, accuracyRoll, evasionRoll);
				
		if (didHit)
		{
			(var strengthRoll, var  defenceRoll, seed) = Rolling.RollPercentilePair(seed);

			var damageDealt = BattleCalculations.CalculateDamage(enemy.Strength, player.Defence, player.Health,
				strengthRoll, defenceRoll);

			info.Add(new EnemySucceededInAttackingPlayer(damageDealt));
			player = player with { Health = player.Health - damageDealt };

			if (player.Health == 0)
			{
				info.Add(new EnemyDefeatedPlayer());
				return new (state with {Seed = seed}, new PlayerDiedMode(), info.ToImmutableArray());
			}

			return new(state with { Seed = seed }, new PlayersTurn(player, enemy), info.ToImmutableArray());
		}

		info.Add(new EnemyFailedInAttackingPlayer());
		return new (state with {Seed = seed},new PlayersTurn(player, enemy), info.ToImmutableArray());
	}
}