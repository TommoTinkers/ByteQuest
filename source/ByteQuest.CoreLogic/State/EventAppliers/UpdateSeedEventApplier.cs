using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.State.EventAppliers;

public static class UpdateSeedEventApplier
{
	public static GameState Apply(this UpdateSeed updateSeedEvent, GameState to) =>
		to with { Seed = updateSeedEvent.newSeed };
}