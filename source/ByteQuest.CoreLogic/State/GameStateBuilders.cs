using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.Ledgers.EventAppliers;

namespace ByteQuest.CoreLogic.State;



public static class GameStateBuilders
{
		
	private static readonly Enemy enemy = new("Goblin", 20, 9, 3, 5, 8);

	private static readonly GameState defaultGameState = new(new Player(20, 20, 3, 4, 20), 10, new PlayersTurn(enemy));
	
	public static GameState CreateFromLedger(GameLedger ledger)
	{
		return ledger.Entries switch
		{
			[] => defaultGameState,
			var entries => entries.Aggregate(defaultGameState, ApplyChange)
		};
	}

	public static GameState ApplyChange(this GameState gameState, Entry entry)
	{
		return entry switch
		{
			UpdateSeedEntry updateSeed => updateSeed.Apply(gameState),
			_ => throw new ArgumentOutOfRangeException(nameof(entry))
		};
	}
}



