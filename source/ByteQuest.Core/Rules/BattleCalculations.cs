namespace ByteQuest.Core.Rules;

public static class BattleCalculations
{
	public static uint CalculateDamage(uint Strength, uint Defence, uint Health, double strengthRoll, double defenceRoll)
	{
		var damageForce = (uint)(Strength * strengthRoll);
		var defenceForce = (uint)(Defence * defenceRoll);

		if (defenceForce > damageForce)
		{
			return 0;
		}

		var damageDealt = damageForce - defenceForce;

		return damageDealt > Health ? Health : damageDealt;
	}

	public static bool CalculateDidHit(uint Accuracy, uint Evasion, double accuracyRoll, double evasionRoll)
	{
		var acc = (uint)(accuracyRoll * Accuracy);
		var eva = (uint)(evasionRoll * Evasion);

		return acc > eva;
	}
}