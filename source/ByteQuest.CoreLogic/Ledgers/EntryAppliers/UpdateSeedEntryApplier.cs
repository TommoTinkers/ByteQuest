using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.Ledgers.EntryAppliers;

public static class UpdateSeedEntryApplier
{
	public static GameState Apply(this UpdateSeedEntry updateSeedEntryEvent, GameState to) =>
		to with { Seed = updateSeedEntryEvent.newSeed };
}