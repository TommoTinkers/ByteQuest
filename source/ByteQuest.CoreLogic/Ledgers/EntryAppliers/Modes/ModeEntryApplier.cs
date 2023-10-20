using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.Ledgers.EntryAppliers.Modes;

public static class ModeEntryApplier
{
	public static Mode ApplyEntry(this Mode mode, UpdateModeEntry entry) => mode;
}