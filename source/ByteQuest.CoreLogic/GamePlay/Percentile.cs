using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.GamePlay;

public static class Percentile
{
	public static (StateAndLedger, double) RollPercentile(this StateAndLedger snl)
	{
		var seed = snl.State.Seed;
		var random = new Random(seed);
		var percentile = random.NextDouble();
		var entry = new UpdateSeedEntry(random.Next());
		snl = snl.RecordEntry(entry);
		return (snl, percentile);
	}
}