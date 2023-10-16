using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.GameRules;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;
using ByteQuest.CoreLogic.State.EventAppliers;

namespace ByteQuest.Shell.Views;

public static class BattleModeView
{
	public static void View(BattleMode battleMode, GameState state, GameLedger ledger)
	{
		switch (battleMode)
		{
			case EnemysTurn enemysTurn:
				PlayEnemysTurn(enemysTurn, state, ledger);
				break;
			case PlayersTurn playersTurn:
				PlayPlayersTurn(playersTurn, state, ledger);
				break;
		}
	}

	private static void PlayEnemysTurn(EnemysTurn enemysTurn, GameState state, GameLedger ledger)
	{
		Console.WriteLine("It is the enemies turn.");
	}

	private static void PlayPlayersTurn(PlayersTurn playersTurn, GameState state, GameLedger ledger)
	{
		var enemy = playersTurn.Enemy;
		Console.WriteLine($"Your health: {state.Player.Health}");
		Console.WriteLine("What do you want to do?");

	
		Console.Write("-> ");
		var input = Console.ReadLine();
		while (input is null)
		{
			Console.Write("-> ");
			input = Console.ReadLine();
		}
		
		switch (input.ToLowerInvariant())
		{
			case "help":
				Console.WriteLine("Available actions");
				Console.WriteLine($"attack - Attack the {enemy.Type} physically.");
				break;
			case "attack":
				Console.WriteLine($"You attempt to attack the {enemy.Type}.");
				var (ev,percentile) = state.RollPercentile();
				ledger = ledger.RecordEntry(ev);
				state = ev.Apply(state);
				
				var didAttackHit = ((double)state.Player.Accuracy / enemy.Evasion) >= percentile;
				if (didAttackHit)
				{
					Console.WriteLine($"You managed to hit the {enemy.Type}");
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
		
	}
}