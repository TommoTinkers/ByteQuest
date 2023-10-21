using ByteQuest.Core.Rules;
using static Turn;

Console.WriteLine("Welcome to ByteQuest!");



var enemy = new { Name = "Bob The Goblin", Health = 10u, Strength = 5u, Accuracy = 5u, Evasion = 5u, Defence = 5u };

var player = new { Health = 10u, Strength = 5u, Accuracy = 5u, Evasion = 5u, Defence = 5u};


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
				var requiredPercentile = 1 - (player.Accuracy / enemy.Evasion);
				(var roll, seed) = Rolling.RollPercentile(seed);

				if (roll >= requiredPercentile)
				{
					Console.WriteLine($"You managed to hit {enemy.Name}");

					(var damageRoll, seed) = Rolling.RollPercentile(seed);
					(var defenceRoll, seed) = Rolling.RollPercentile(seed);
					var damageForce = (uint)(damageRoll * player.Strength);
					var blockingForce = (uint)(defenceRoll * enemy.Defence);
					var damage = blockingForce > damageForce ? 0 : damageForce - blockingForce;
					
					var damageDealt = Math.Min(enemy.Health, damage);

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
		var requiredPercentile = 1 - (enemy.Accuracy / player.Evasion);
		(var roll, seed) = Rolling.RollPercentile(seed);
		if (roll >= requiredPercentile)
		{
			Console.WriteLine($"You have been hit!");

			(var damageRoll, seed) = Rolling.RollPercentile(seed);
			(var defenceRoll, seed) = Rolling.RollPercentile(seed);
			var damageForce = (uint)(damageRoll * enemy.Strength);
			var blockingForce = (uint)(defenceRoll * player.Defence);
			var damage = blockingForce > damageForce ? 0 : damageForce - blockingForce;
					
			var damageDealt = Math.Min(player.Health, damage);

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

		turn = Turn.Player;
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