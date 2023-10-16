using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Events;
using ByteQuest.CoreLogic.State;
using static ByteQuest.CoreLogic.State.GameStateBuilders;


Console.WriteLine("Welcome to ByteQuest!");


var enemy = new Enemy("Goblin", 20, 9, 3, 5, 8);


var events = ImmutableArray<Event>.Empty;

events = events.Add(new CreatePlayerEvent(50, 12, 7, 2, 3));

var initializationResult = InitializeFromEvents(events);

if (initializationResult is not GameStateInitialized initialized)
{
	return;
}

var player = initialized.State.Player;


Console.WriteLine($"A {enemy.Type} blocks your path.");

while (true)
{
	Console.WriteLine($"Your health: {player.Health}");
	Console.WriteLine("What do you want to do?");

	
	Console.Write("-> ");
	var input = Console.ReadLine();
	while (input is null)
	{
		Console.Write("-> ");
		input =Console.ReadLine();
	}
	
	switch (input.ToLowerInvariant())
	{
		case "help":
			Console.WriteLine("Available actions");
			Console.WriteLine($"attack - Attack the {enemy.Type} physically.");
			break;
		case "attack":
			Console.WriteLine($"You attempt to attack the {enemy.Type}.");
			var percentile = Random.Shared.NextDouble();
			var didAttackHit = ((double)player.Accuracy / enemy.Evasion) >= percentile;
			if (didAttackHit)
			{
				Console.WriteLine($"You managed to hit the {enemy.Type}");
				var defencePercentile = Random.Shared.NextDouble();
				var strengthPercentile = Random.Shared.NextDouble();
				var damage = (uint)Math.Max(1d, (player.Strength * strengthPercentile) - (enemy.Defence * defencePercentile));
				enemy = enemy with {Health = enemy.Health <= damage ? 0 : enemy.Health - damage };
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

	if (enemy.Health == 0)
	{
		Console.WriteLine($"The {enemy.Type} is dead.");
	}
	else
	{
		Console.WriteLine($"The {enemy.Type} retaliates.");
		var percentile = Random.Shared.NextDouble();
		var didAttackHit = ((double)enemy.Accuracy / player.Evasion) >= percentile;
		if (didAttackHit)
		{
			Console.WriteLine($"The {enemy.Type} managed to hit you!");
			var defencePercentile = Random.Shared.NextDouble();
			var strengthPercentile = Random.Shared.NextDouble();
			var damage = (uint)Math.Max(1d, (enemy.Strength * strengthPercentile) - (player.Defence * defencePercentile));
			player = player with { Health = player.Health <= damage ? 0 : player.Health - damage };
			Console.WriteLine($"You suffered {damage} points of damage.");
		}
		else
		{
			Console.WriteLine("But he misses!");
		}
	}

	if (player.Health == 0)
	{
		Console.WriteLine("You died");
		break;
	}
}
