using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.State.EventAppliers;

public static class UpdateSeedEventApplier
{
	public static GameState Apply(this UpdateSeedEntry updateSeedEntryEvent, GameState to) =>
		to with { Seed = updateSeedEntryEvent.newSeed };
}