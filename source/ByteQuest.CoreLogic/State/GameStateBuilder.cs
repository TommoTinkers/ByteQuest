using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.Ledgers.EntryAppliers;

namespace ByteQuest.CoreLogic.State;



public static class GameStateBuilder
{

	private static readonly Enemy enemy = new("Goblin", 20, 9, 3, 5, 8);

	private static readonly GameState defaultGameState = new(new Player(20, 20, 3, 4, 20), 10, new PlayersTurn(enemy));

	public static GameState CreateFromLedger(GameLedger ledger)
	{
		return ledger.Entries switch
		{
			[] => defaultGameState,
			var entries => entries.Aggregate(defaultGameState, GameStateEntryApplier.ApplyEntry)
		};
	}
}


