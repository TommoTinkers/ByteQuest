using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers.EntryAppliers.Modes;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.Ledgers.EntryAppliers;

public static class GameStateEntryApplier
{
	public static GameState ApplyEntry(this GameState gameState, Entry entry)
	{
		return entry switch
		{
			UpdateSeedEntry updateSeed => updateSeed.Apply(gameState),
			UpdateModeEntry modeEntry => gameState with {Mode = gameState.Mode.ApplyEntry(modeEntry)}, 
			_ => throw new ArgumentOutOfRangeException(nameof(entry))
		};
	}
}
