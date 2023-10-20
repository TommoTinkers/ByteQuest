using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.GamePlay.Updaters;

namespace ByteQuest.CoreLogic.Ledgers.EntryAppliers.Modes.Battle;

public static class BattleModeEntryApplier
{
	public static BattleMode ApplyBattleModeEntry(this BattleMode mode, BattleModeEntry entry) => entry switch
	{
		DamageEnemyEntry damageEnemyEntry => mode with { Enemy = mode.Enemy.Damage(damageEnemyEntry) },
		_ => throw new ArgumentOutOfRangeException(nameof(entry))
	};
}