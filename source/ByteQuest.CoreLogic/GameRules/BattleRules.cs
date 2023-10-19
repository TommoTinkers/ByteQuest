namespace ByteQuest.CoreLogic.GameRules;

public static class BattleRules
{
	public static double AttackSuccessPercentile(uint accuracy, uint evasion) => 1 - (double)accuracy / evasion;

	public static uint CalculateDamage(uint playerStrength, uint enemyDefence, double damagePercentile, double defencePercentile) =>
		((uint)(playerStrength * damagePercentile), (uint)(enemyDefence * defencePercentile)) switch
		{
			var (dmg, blk) when blk > dmg => 0,
			var (dmg, blk) => dmg - blk,
		};
}