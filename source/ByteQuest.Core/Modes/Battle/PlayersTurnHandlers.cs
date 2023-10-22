using System.Collections.Immutable;
using ByteQuest.Core.Data;
using ByteQuest.Core.Rules;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;

public static class PlayersTurnHandlers
{
	public static BattleModeResult<AttackResult> Attack(GameState state, PlayersTurn mode)
	{
		var enemy = mode.Enemy;
		var seed = state.Seed;
		var player = mode.Player;

		var info = new List<AttackResult>();
		
		info.Add(new AttackAttempted(enemy.Name));
		(var accuracyRoll, var evasionRoll, seed) = Rolling.RollPercentilePair(seed);

		var didHit = BattleCalculations.CalculateDidHit(player.Accuracy, enemy.Evasion, accuracyRoll, evasionRoll);

		return didHit
			? AttackSucceeded(state, seed, player, enemy, info)
			: AttackFailed(state, info, seed, player, enemy);
	}

	private static BattleModeResult<AttackResult> AttackSucceeded(GameState state, int seed, Player player, Enemy enemy, ICollection<AttackResult> info)
	{
		(var strengthRoll, var defenceRoll, seed) = Rolling.RollPercentilePair(seed);


		var damageDealt = BattleCalculations.CalculateDamage(player.Strength, enemy.Defence, enemy.Health,
			strengthRoll, defenceRoll);

		info.Add(new AttackSucceeded(damageDealt));
		enemy = enemy with { Health = enemy.Health - damageDealt };

		if (enemy.Health == 0)
		{
			info.Add(new EnemyWasDefeated(enemy.Name));
			return new BattleModeResult<AttackResult>(state with { Seed = seed }, new ExitGameMode(), info.ToImmutableArray());
		}

		return new BattleModeResult<AttackResult>(state with { Seed = seed }, new EnemiesTurn(player, enemy), info.ToImmutableArray());
	}

	private static BattleModeResult<AttackResult> AttackFailed(GameState state, ICollection<AttackResult> info, int seed, Player player, Enemy enemy)
	{
		info.Add(new AttackFailed());
		return new(state with { Seed = seed }, new EnemiesTurn(player, enemy), info.ToImmutableArray());
	}
}