namespace ByteQuest.Core.Rules;

public static class Rolling
{
	public static (double Percentile, int NewSeed) RollPercentile(int seed)
	{
		var random = new Random(seed);
		return (random.NextDouble(), random.Next());
	}
}