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
				
		var seed = Random.Shared.Next();
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
				return (state, new PlayerDiedMode());
			}
		}
		else
		{
			Console.WriteLine($"He missed!");
		}

		return (state,new PlayersTurn(player, enemy));
	}

	private static (GameState,Mode) ViewPlayersTurn(GameState state, PlayersTurn mode)
	{
		
		var seed = Random.Shared.Next();
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
					Console.WriteLine($"You attack {enemy.Name}");
					(var accuracyRoll, var evasionRoll, seed) = Rolling.RollPercentilePair(seed);

					var didHit = CalculateDidHit(player.Accuracy, enemy.Evasion, accuracyRoll, evasionRoll);

					if (didHit)
					{
						Console.WriteLine($"You managed to hit {enemy.Name}");

						(var strengthRoll, var defenceRoll, seed) = Rolling.RollPercentilePair(seed);


						var damageDealt = CalculateDamage(player.Strength, enemy.Defence, enemy.Health,
							strengthRoll, defenceRoll);

						Console.WriteLine($"You did {damageDealt} points of damage to {enemy.Name}");
						enemy = enemy with { Health = enemy.Health - damageDealt };
						
						if (enemy.Health == 0)
						{
							Console.WriteLine($"You defeated {enemy.Name}");
							return (state, new ExitGameMode());
						}
						
						return (state, new EnemiesTurn(player, enemy));
					}

					Console.WriteLine($"You missed!");



					return (state, new EnemiesTurn(player, enemy));

				default:
					Console.WriteLine("I dont understand what you want");
					break;
			}
		}
	}
}