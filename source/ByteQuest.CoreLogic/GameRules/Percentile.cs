using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.GameRules;

public static class Percentile
{
	public static (UpdateSeedEntry, double) RollPercentile(this GameState state)
	{
		var seed = state.Seed;
		var random = new Random(seed);
		var percentile = random.NextDouble();
		return (new UpdateSeedEntry(random.Next(), new Guid()), percentile);
	}
}