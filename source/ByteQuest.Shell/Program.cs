using ByteQuest.Core.Rules;
using static ByteQuest.Core.Rules.BattleCalculations;
using static Turn;

Console.WriteLine("Welcome to ByteQuest!");



var enemy = new { Name = "Bob The Goblin", Health = 10u, Strength = 5u, Accuracy = 5u, Evasion = 5u, Defence = 5u };

var player = new { Health = 10u, Strength = 5u, Accuracy = 6u, Evasion = 6u, Defence = 6u};


Console.WriteLine($"You are accosted by {enemy.Name}");

var seed = Random.Shared.Next();

var turn = Player;

while (true)
{
	if (turn == Player)
	{
		Console.WriteLine($"Your health: [{player.Health}]. Enemy health: [{enemy.Health}]");
		Console.WriteLine("It is your turn. What do you want to do.");
		
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

					(var strengthRoll, var  defenceRoll, seed) = Rolling.RollPercentilePair(seed);


					var damageDealt = CalculateDamage(player.Strength, enemy.Defence, enemy.Health,
						strengthRoll, defenceRoll);

					Console.WriteLine($"You did {damageDealt} points of damage to {enemy.Name}");
					enemy = enemy with { Health = enemy.Health - damageDealt };
				}
				else
				{
					Console.WriteLine($"You missed!");
				}

				turn = Enemy;

				if (enemy.Health == 0)
				{
					Console.WriteLine($"You defeated {enemy.Name}");
					return;
				}
				break;

			default:
				Console.WriteLine("I dont understand what you want");
				break;
		}
	}
	else
	{
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
				return;
			}
		}
		else
		{
			Console.WriteLine($"He missed!");
		}

		turn = Player;
	}
}


static string GetLine()
{
	Console.Write("-> ");
	var line = Console.ReadLine();
	while (line == null)
	{
		Console.Write("-> ");
		line = Console.ReadLine();
	}

	return line;
}

internal enum Turn
{
	Player,
	Enemy
}