using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;
using ByteQuest.Shell.Views;
using static ByteQuest.CoreLogic.State.GameStateBuilders;


Console.WriteLine("Welcome to ByteQuest!");


var ledger = new GameLedger(ImmutableArray<Entry>.Empty);


var state = CreateFromLedger(ledger);

if (state.Mode is not BattleMode battleMode)
{
	return;
}

BattleModeView.View(battleMode, state, ledger);


/*
Console.WriteLine($"A {battleMode.Enemy.Type} blocks your path.");

while (true)
{

	switch (input.ToLowerInvariant())
	{
		case "help":
			Console.WriteLine("Available actions");
			Console.WriteLine($"attack - Attack the {battleMode.Enemy.Type} physically.");
			break;
		case "attack":
			Console.WriteLine($"You attempt to attack the {battleMode.Enemy.Type}.");
			var percentile = Random.Shared.NextDouble();
			var didAttackHit = ((double)state.Player.Accuracy / battleMode.Enemy.Evasion) >= percentile;
			if (didAttackHit)
			{
				Console.WriteLine($"You managed to hit the {battleMode.Enemy.Type}");
				var defencePercentile = Random.Shared.NextDouble();
				var strengthPercentile = Random.Shared.NextDouble();
				var damage = (uint)Math.Max(1d, (state.Player.Strength * strengthPercentile) - (state.Player.Defence * defencePercentile));
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

	if (battleMode.Enemy.Health == 0)
	{
		Console.WriteLine($"The {battleMode.Enemy.Type} is dead.");
	}
	else
	{
		Console.WriteLine($"The {battleMode.Enemy.Type} retaliates.");
		var percentile = Random.Shared.NextDouble();
		var didAttackHit = ((double)battleMode.Enemy.Accuracy / state.Player.Evasion) >= percentile;
		if (didAttackHit)
		{
			Console.WriteLine($"The {battleMode.Enemy.Type} managed to hit you!");
			var defencePercentile = Random.Shared.NextDouble();
			var strengthPercentile = Random.Shared.NextDouble();
			var damage = (uint)Math.Max(1d, (battleMode.Enemy.Strength * strengthPercentile) - (state.Player.Defence * defencePercentile));
			player = player with { Health = player.Health <= damage ? 0 : player.Health - damage };
			Console.WriteLine($"You suffered {damage} points of damage.");
		}
		else
		{
			Console.WriteLine("But he misses!");
		}
	}

	if (state.Player.Health == 0)
	{
		Console.WriteLine("You died");
		break;
	}
}*/
