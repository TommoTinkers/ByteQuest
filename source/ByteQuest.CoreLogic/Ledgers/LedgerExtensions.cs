using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers.EntryAppliers;

namespace ByteQuest.CoreLogic.Ledgers;

public static class LedgerExtensions
{

	public static StateAndLedger RecordEntry(this StateAndLedger snl, Entry entry) =>
		new(State: snl.State.ApplyEntry(entry), Ledger: new GameLedger(Entries: snl.Ledger.Entries.Add(entry)));
}