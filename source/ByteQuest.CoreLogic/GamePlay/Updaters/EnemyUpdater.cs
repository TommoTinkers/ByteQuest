using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.GamePlay.Updaters;

public static class EnemyUpdater
{
	public static Enemy Damage(this Enemy enemy, DamageEnemyEntry entry) =>
		enemy with { Health = enemy.Health - entry.Amount };
}