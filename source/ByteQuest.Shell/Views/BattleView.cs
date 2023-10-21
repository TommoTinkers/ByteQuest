using ByteQuest.Core.Modes;
using ByteQuest.Core.Rules;
using static ByteQuest.Core.Rules.BattleCalculations;
using static ByteQuest.Shell.Shell;

namespace ByteQuest.Shell.Views;

public static class BattleView
{
	public static Mode View(BattleMode mode)
	{
		while (true)
		{
			mode = mode switch
			{
				EnemiesTurn enemiesTurn => ViewEnemiesTurn(enemiesTurn),
				PlayersTurn playersTurn => ViewPlayersTurn(playersTurn),
				_ => throw new ArgumentOutOfRangeException(nameof(mode))
			};
		}
	}

	private static BattleMode ViewEnemiesTurn(EnemiesTurn mode)
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
				return mode;
			}
		}
		else
		{
			Console.WriteLine($"He missed!");
		}

		return new PlayersTurn(player, enemy);
	}

	private static BattleMode ViewPlayersTurn(PlayersTurn mode)
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
						return new EnemiesTurn(player, enemy);
					}

					Console.WriteLine($"You missed!");

					if (enemy.Health == 0)
					{
						Console.WriteLine($"You defeated {enemy.Name}");
						return mode;
					}

					return new EnemiesTurn(player, enemy);

				default:
					Console.WriteLine("I dont understand what you want");
					break;
			}
		}
	}
}