using ByteQuest.Core.Modes;
using ByteQuest.Core.Rules;
using ByteQuest.Core.State;
using static ByteQuest.Core.Rules.BattleCalculations;
using static ByteQuest.Shell.Shell;

namespace ByteQuest.Shell.Views;

public static class BattleView
{
	public static (GameState, Mode) View(GameState state, BattleMode mode)
	{
			return mode switch
			{
				EnemiesTurn enemiesTurn => ViewEnemiesTurn(state, enemiesTurn),
				PlayersTurn playersTurn => ViewPlayersTurn(state, playersTurn),
				_ => throw new ArgumentOutOfRangeException(nameof(mode))
			};
	}

	private static (GameState, Mode) ViewEnemiesTurn(GameState state, EnemiesTurn mode)
	{
				
		var seed = state.Seed;
		var player = mode.Player;
		var enemy = mode.Enemy;

		
		Console.WriteLine($"{enemy.Name} tries to attack you!");
		(var accuracyRoll, var evasionRoll, seed) = Rolling.RollPercentilePair(seed);
				

		var didHit = CalculateDidHit(enemy.Accuracy, player.Evasion, accuracyRoll, evasionRoll);
				
		if (didHit)
		{
			Console.WriteLine($"You have been hit!");

			(var strengthRoll, var  defenceRoll, seed) = Rolling.RollPercentilePair(seed);
					

			var damageDealt = CalculateDamage(enemy.Strength, player.Defence, player.Health,
				strengthRoll, defenceRoll);

			Console.WriteLine($"You suffered {damageDealt} points of damage.");
			player = player with { Health = player.Health - damageDealt };

			if (player.Health == 0)
			{
				Console.WriteLine($"You died.");
				return (state with {Seed = seed}, new PlayerDiedMode());
			}
		}
		else
		{
			Console.WriteLine($"He missed!");
		}

		return (state with {Seed = seed},new PlayersTurn(player, enemy));
	}

	private static (GameState,Mode) ViewPlayersTurn(GameState state, PlayersTurn mode)
	{
		
		var player = mode.Player;
		var enemy = mode.Enemy;
		
		Console.WriteLine($"Your health: [{player.Health}]. Enemy health: [{enemy.Health}]");
		Console.WriteLine("It is your turn. What do you want to do.");
		
		while(true)
		{
			var input = GetLine();
			switch (input.ToLowerInvariant())
			{
				case "attack":
					var result = BattleModeHandlers.Attack(state, mode);

					foreach (var info in result.Info)
					{
						var msg = info switch
						{
							AttackFailed => "You missed.",
							AttackAttempted attackAttempted => $"You attempt to strike {attackAttempted.EnemyName}",
							AttackSucceeded(var damage) => $"You inflicted {damage} points of damage!",
							EnemyWasDefeated(var enemyName) => $"You defeated {enemyName} !",
							var x => $"Error: No message for {x}"
						};
					}
					return (result.state, result.nextMode);
					
					
				default:
					Console.WriteLine("I dont understand what you want");
					break;
			}
		}
	}
}