namespace ByteQuest.Core.Rules;

public static class Rolling
{
	public static (double Percentile, int NewSeed) RollPercentile(int seed)
	{
		var random = new Random(seed);
		return (random.NextDouble(), random.Next());
	}

	public static (double first, double second, int newSeed) RollPercentilePair(int seed)
	{
		(var first, seed) = RollPercentile(seed);
		(var second, seed) = RollPercentile(seed);
		return (first, second, seed);
	}
}