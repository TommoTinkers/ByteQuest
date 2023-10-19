using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.GamePlay;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;
using static ByteQuest.CoreLogic.GamePlay.Attacking;

namespace ByteQuest.Shell.Views;

public static class BattleModeView
{
	public static StateAndLedger View(BattleMode battleMode, StateAndLedger snl)
	{
		switch (battleMode)
		{
			case EnemysTurn enemysTurn:
				PlayEnemysTurn(enemysTurn, snl);
				break;
			case PlayersTurn playersTurn:
				snl = PlayPlayersTurn(playersTurn, snl);
				break;
		}

		return snl;
	}

	private static void PlayEnemysTurn(EnemysTurn enemysTurn, StateAndLedger snl)
	{
		Console.WriteLine("It is the enemies turn.");
	}

	private static StateAndLedger PlayPlayersTurn(PlayersTurn playersTurn, StateAndLedger snl)
	{
		var enemy = playersTurn.Enemy;
		Console.WriteLine($"Your health: {snl.State.Player.Health}");
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
				(snl, var results) = playersTurn.Attack(snl);

				foreach (var result in results.Info)
				{
					var message = result switch
					{
						AttackAttempted => "You attempt an attack.",
						AttackFailed => "You missed.",
						AttackSucceeded => "You managed to strike the enemy.",
						DamageDealt(var amount, var remaining) => $"You inflicted {amount} points of damage. The enemy has {remaining} health points left.", 
						_ => throw new ArgumentOutOfRangeException(nameof(result))
					};
					Console.WriteLine(message);
				}
									
				
				

/*					Console.WriteLine($"You managed to hit the {enemy.Type}");
					var defencePercentile = Random.Shared.NextDouble();
					var strengthPercentile = Random.Shared.NextDouble();
					var damage = (uint)Math.Max(1d, (snl.State.Player.Strength * strengthPercentile) - (snl.State.Player.Defence * defencePercentile));
					enemy = enemy with {Health = enemy.Health <= damage ? 0 : enemy.Health - damage };
					Console.WriteLine($"You caused {damage} points of damage.");
				
			
					Console.WriteLine("Your attack missed.");
				*/
				break;			
			default:
				Console.WriteLine("I did not understand this input");
				break;
		}

		return snl;
	}
	
}