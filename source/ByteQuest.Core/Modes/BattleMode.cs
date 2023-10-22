using System.Collections.Immutable;
using ByteQuest.Core.Data;
using ByteQuest.Core.Rules;
using ByteQuest.Core.State;
using static ByteQuest.Core.Rules.BattleCalculations;

namespace ByteQuest.Core.Modes;

public abstract record BattleMode(Player Player, Enemy Enemy) : Mode;

public sealed record PlayersTurn(Player Player, Enemy Enemy) : BattleMode(Player, Enemy);

public sealed record EnemiesTurn(Player Player, Enemy Enemy) : BattleMode(Player, Enemy);


public abstract record BattleModeResultInfo;

public abstract record AttackResult : BattleModeResultInfo;

public sealed record AttackAttempted(string EnemyName) : AttackResult;

public sealed record AttackFailed : AttackResult;
public sealed record AttackSucceeded(uint damage) : AttackResult;

public sealed record EnemyWasDefeated(string enemyName) : AttackResult;

public sealed record BattleModeResult(GameState state, Mode nextMode, ImmutableArray<BattleModeResultInfo> Info);

public static class BattleModeHandlers
{
	public static BattleModeResult Attack(GameState state, PlayersTurn mode)
	{
		var enemy = mode.Enemy;
		var seed = state.Seed;
		var player = mode.Player;

		var info = new List<BattleModeResultInfo>();
		
		info.Add(new AttackAttempted(enemy.Name));
		(var accuracyRoll, var evasionRoll, seed) = Rolling.RollPercentilePair(seed);

		var didHit = CalculateDidHit(player.Accuracy, enemy.Evasion, accuracyRoll, evasionRoll);

		return didHit
			? AttackSucceeded(state, seed, player, enemy, info)
			: AttackFailed(state, info, seed, player, enemy);
	}

	private static BattleModeResult AttackSucceeded(GameState state, int seed, Player player, Enemy enemy, ICollection<BattleModeResultInfo> info)
	{
		(var strengthRoll, var defenceRoll, seed) = Rolling.RollPercentilePair(seed);


		var damageDealt = CalculateDamage(player.Strength, enemy.Defence, enemy.Health,
			strengthRoll, defenceRoll);

		info.Add(new AttackSucceeded(damageDealt));
		enemy = enemy with { Health = enemy.Health - damageDealt };

		if (enemy.Health == 0)
		{
			info.Add(new EnemyWasDefeated(enemy.Name));
			return new BattleModeResult(state with { Seed = seed }, new ExitGameMode(), info.ToImmutableArray());
		}

		return new BattleModeResult(state with { Seed = seed }, new EnemiesTurn(player, enemy), info.ToImmutableArray());
	}

	private static BattleModeResult AttackFailed(GameState state, List<BattleModeResultInfo> info, int seed, Player player, Enemy enemy)
	{
		info.Add(new AttackFailed());
		return new(state with { Seed = seed }, new EnemiesTurn(player, enemy), info.ToImmutableArray());
	}
}
