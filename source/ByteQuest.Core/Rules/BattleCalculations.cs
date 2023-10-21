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
}