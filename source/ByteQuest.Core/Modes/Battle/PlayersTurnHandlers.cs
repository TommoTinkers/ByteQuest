using System.Collections.Immutable;
using ByteQuest.Core.Commands;
using ByteQuest.Core.Data;
using ByteQuest.Core.Rules;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Modes.Battle;


public sealed record PlayerCommandInfo
	(string Name, Handler<BattleModeResult<PlayerTurnResult>, PlayersTurn> Handler) :
		CommandInfo<BattleModeResult<PlayerTurnResult>, PlayersTurn>(Name, Handler);

public static class PlayersTurnHandlers
{
	public static List<PlayerCommandInfo> AvailableCommands = new()
	{
		new PlayerCommandInfo("attack", Attack)
	};
	
	public static BattleModeResult<PlayerTurnResult> Attack(GameState state, PlayersTurn mode)
	{
		var enemy = mode.Enemy;
		var seed = state.Seed;
		var player = mode.Player;

		var info = new List<PlayerTurnResult>();
		
		info.Add(new PlayerTurnAttempted(enemy.Name));
		(var accuracyRoll, var evasionRoll, seed) = Rolling.RollPercentilePair(seed);

		var didHit = BattleCalculations.CalculateDidHit(player.Accuracy, enemy.Evasion, accuracyRoll, evasionRoll);

		return didHit
			? DealDamageToEnemy(state, seed, player, enemy, info)
			: ReportAttackFailed(state, info, seed, player, enemy);
	}

	private static BattleModeResult<PlayerTurnResult> DealDamageToEnemy(GameState state, int seed, Player player, Enemy enemy, ICollection<PlayerTurnResult> info)
	{
		(var strengthRoll, var defenceRoll, seed) = Rolling.RollPercentilePair(seed);


		var damageDealt = BattleCalculations.CalculateDamage(player.Strength, enemy.Defence, enemy.Health,
			strengthRoll, defenceRoll);

		info.Add(new PlayerTurnSucceeded(damageDealt));
		enemy = enemy with { Health = enemy.Health - damageDealt };

		if (enemy.Health == 0)
		{
			info.Add(new EnemyWasDefeated(enemy.Name));
			return new BattleModeResult<PlayerTurnResult>(state with { Seed = seed }, new ExitGameMode(), info.ToImmutableArray());
		}

		return new BattleModeResult<PlayerTurnResult>(state with { Seed = seed }, new EnemiesTurn(player, enemy), info.ToImmutableArray());
	}

	private static BattleModeResult<PlayerTurnResult> ReportAttackFailed(GameState state, ICollection<PlayerTurnResult> info, int seed, Player player, Enemy enemy)
	{
		info.Add(new PlayerTurnFailed());
		return new(state with { Seed = seed }, new EnemiesTurn(player, enemy), info.ToImmutableArray());
	}
}