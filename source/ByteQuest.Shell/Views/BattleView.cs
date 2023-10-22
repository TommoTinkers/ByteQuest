using ByteQuest.Core.Modes;
using ByteQuest.Core.Modes.Battle;
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

		var result = EnemiesTurnHandler.Attack(state, mode);
		foreach (var info in result.Info)
		{
			var msg = info switch
			{
				EnemyAttemptsToAttack(var enemyName) => $"{enemyName} attempts to attack you.",
				EnemyFailedInAttackingPlayer => "But they missed.",
				EnemySucceededInAttackingPlayer(var damage) => $"The hit you and do {damage} points of damage.",
				EnemyDefeatedPlayer => $"You died.",
				var x => $"Error: No message for {x}"
			};
						
			Console.WriteLine(msg);
		}

		return (result.state, result.nextMode);
	}

	private static (GameState,Mode) ViewPlayersTurn(GameState state, PlayersTurn mode)
	{
		
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
					var result = PlayersTurnHandlers.Attack(state, mode);

					foreach (var info in result.Info)
					{
						var msg = info switch
						{
							AttackFailed => "You missed.",
							AttackAttempted attackAttempted => $"You attempt to strike {attackAttempted.EnemyName}",
							AttackSucceeded(var damage) => $"You inflicted {damage} points of damage!",
							EnemyWasDefeated(var enemyName) => $"You defeated {enemyName} !",
							var x => $"Error: No message for {x}"
						};
						
						Console.WriteLine(msg);
					}
					return (result.state, result.nextMode);
					
					
				default:
					Console.WriteLine("I dont understand what you want");
					break;
			}
		}
	}
}