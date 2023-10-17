namespace ByteQuest.CoreLogic.GameRules;

public static class BattleRules
{
	public static double AttackSuccessPercentile(uint accuracy, uint evasion) => 1 - (double)accuracy / evasion;
}