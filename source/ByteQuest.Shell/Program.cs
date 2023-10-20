Console.WriteLine("Welcome to ByteQuest!");



var enemy = new { Name = "Bob The Goblin", Health = 10u, Strength = 5u, Accuracy = 5u, Evasion = 5u, Defence = 5u };

var player = new { Health = 10u, Strengh = 5u, Accuracy = 5u, Evasion = 5u, Defence = 5u};


Console.WriteLine($"You are accosted by {enemy.Name}");

var seed = 10;
while (true)
{
	Console.WriteLine("What do you want to do?");
	var input = GetLine();

	switch (input.ToLowerInvariant())
	{
		case "attack":
			Console.WriteLine($"You attack {enemy.Name}");
			var requiredPercentile = 1 - (player.Accuracy / enemy.Evasion);
			(var roll, seed) = RollPercentile(seed);

			if (roll >= requiredPercentile)
			{
				Console.WriteLine($"You managed to hit {enemy.Name}");
			}
			break;
		
		default:
			Console.WriteLine("I dont understand what you want");
			break;
		
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

static (double Percentile, int NewSeed) RollPercentile(int seed)
{
	var random = new Random(seed);
	return (random.NextDouble(), random.Next());
}