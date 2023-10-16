Console.WriteLine("Welcome to ByteQuest!");

var enemyType = "Goblin";

var enemyEvasion = 5d;
var enemyAccuracy = 3d;
var enemyDefence = 8d;
var enemyStrength = 9d;

var enemyHealth = 20;

var playerHealth = 5;
var playerAccuracy = 4d;
var playerEvasion = 6;
var playerStrength = 8d;
var playerDefence = 11d;

Console.WriteLine($"A {enemyType} blocks your path.");

while (true)
{
	Console.WriteLine($"Your health: {playerHealth}");
	Console.WriteLine("What do you want to do?");

	Console.Write("-> ");
	var input = Console.ReadLine();

	switch (input.ToLowerInvariant())
	{
		case "help":
			Console.WriteLine("Available actions");
			Console.WriteLine($"attack - Attack the {enemyType} physically.");
			break;
		case "attack":
			Console.WriteLine($"You attempt to attack the {enemyType}.");
			var percentile = Random.Shared.NextDouble();
			var didAttackHit = (playerAccuracy / enemyEvasion) >= percentile;
			if (didAttackHit)
			{
				Console.WriteLine($"You managed to hit the {enemyType}");
				var defencePercentile = Random.Shared.NextDouble();
				var strengthPercentile = Random.Shared.NextDouble();
				var damage = (int)Math.Max(1d, (playerStrength * strengthPercentile) - (enemyDefence * defencePercentile));
				enemyHealth = enemyHealth <= damage ? 0 : enemyHealth - damage;
				Console.WriteLine($"You caused {damage} points of damage.");
			}
			else
			{
				Console.WriteLine("Your attack missed.");
			}
			break;			
		default:
			Console.WriteLine("I did not understand this input");
			break;
	}

	if (enemyHealth == 0)
	{
		Console.WriteLine($"The {enemyType} is dead.");
	}
	else
	{
		Console.WriteLine($"The {enemyType} retaliates.");
		var percentile = Random.Shared.NextDouble();
		var didAttackHit = (enemyAccuracy / playerEvasion) >= percentile;
		if (didAttackHit)
		{
			Console.WriteLine($"The {enemyType} managed to hit you!");
			var defencePercentile = Random.Shared.NextDouble();
			var strengthPercentile = Random.Shared.NextDouble();
			var damage = (int)Math.Max(1d, (enemyStrength * strengthPercentile) - (playerDefence * defencePercentile));
			playerHealth = playerHealth <= damage ? 0 : playerHealth - damage;
			Console.WriteLine($"You suffered {damage} points of damage.");
		}
		else
		{
			Console.WriteLine("But he misses!");
		}
	}

	if (playerHealth == 0)
	{
		Console.WriteLine("You died");
		break;
	}
}