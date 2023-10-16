using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.Ledgers;

public static class LedgerExtensions
{
	public static GameLedger RecordEntry(this GameLedger ledger, Entry entry) => new(Entries: ledger.Entries.Add(entry));
}