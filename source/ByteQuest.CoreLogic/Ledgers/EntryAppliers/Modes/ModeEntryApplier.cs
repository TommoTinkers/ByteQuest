using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers.EntryAppliers.Modes.Battle;

namespace ByteQuest.CoreLogic.Ledgers.EntryAppliers.Modes;

public static class ModeEntryApplier
{
	public static Mode ApplyEntry(this Mode mode, UpdateModeEntry entry) => (mode,entry) switch
	{
		(BattleMode battleMode, BattleModeEntry battleModeEntry) => battleMode.ApplyBattleModeEntry(battleModeEntry),
		_ => mode
	};
}