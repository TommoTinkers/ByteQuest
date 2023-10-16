using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.Ledgers;

public static class LedgerExtensions
{
	public static GameLedger RecordEntry(this GameLedger ledger, Entry entry)
	{
		if (ledger.Entries.Select(e => e.id).Contains(entry.id))
		{
			return ledger;
		}
		return new GameLedger(Entries: ledger.Entries.Add(entry));
	}
}